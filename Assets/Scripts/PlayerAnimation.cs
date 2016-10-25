using UnityEngine;
using System.Collections;
using System;

public enum AnimationState {
    Idel,
    Run,
    Left,
    Right,
    Sliding,
    Jump,
    Death,
}

public class PlayerAnimation : MonoBehaviour {
    public AnimationState animationState = AnimationState.Idel;
    private Animation mAnimation;
    private PlayerMove playerMove;
    public AudioSource footAudio;
    public bool mIsPlayDeathAnimation = false;
	// Use this for initialization
	void Start () {
        mAnimation = transform.Find("Prisoner").GetComponent<Animation>();
        playerMove = GetComponent<PlayerMove>();

    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.mGameState == GameState.Menu) {
            animationState = AnimationState.Idel;
        } else if (GameController.mGameState == GameState.Play) {
            animationState = AnimationState.Run;
            if (playerMove.mIsSliding) {
                animationState = AnimationState.Sliding;
            }
            if (playerMove.mIsJump) {
                animationState = AnimationState.Jump;
            }
            if (animationState == AnimationState.Run &&  !footAudio.isPlaying) {
                footAudio.Play();
            }
            if (animationState != AnimationState.Run) {
                footAudio.Stop();
            }
        } else if (GameController.mGameState == GameState.End) {
            animationState = AnimationState.Death;
            PlayDeathAnimation();
        }

	}

    private void PlayDeathAnimation() {
        if (!mAnimation.IsPlaying("death")&& !mIsPlayDeathAnimation) {
            PlayAnimation("death");
            mIsPlayDeathAnimation = true;
        }
    }

    void LateUpdate() {
        switch (animationState) {
            case AnimationState.Run:
                PlayAnimation("run");
                break;
            case AnimationState.Idel:
                PlayerIdle();
                break;
            case AnimationState.Sliding:
                PlayAnimation("slide");
                break;
            case AnimationState.Jump:
                break;
            default:
                break;
        }
    }

    public void PlayAnimation(string v)
    {
        if (!mAnimation.IsPlaying(v)) {
            mAnimation.Play(v);
        }
    }

    private void PlayerIdle()
    {
        if (!mAnimation.IsPlaying("Idle_1") && !mAnimation.IsPlaying("Idle_2") ) {
            mAnimation.Play("Idle_1");
            mAnimation.PlayQueued("Idle_2");
        }
    }
}
