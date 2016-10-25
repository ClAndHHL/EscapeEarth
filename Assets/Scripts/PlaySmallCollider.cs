using UnityEngine;
using System.Collections;

public class PlaySmallCollider : MonoBehaviour {

    // Use this for initialization
    private PlayerAnimation playerAnimation;
    void Start() {
        playerAnimation = GameObject.FindGameObjectWithTag(TagsLayer.Player).GetComponent<PlayerAnimation>();
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.tag == TagsLayer.Obstacle && GameController.mGameState == GameState.Play && playerAnimation.animationState == AnimationState.Sliding) {
            GameController.mGameState = GameState.End;
        }
    }
}
