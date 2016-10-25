using UnityEngine;
using System.Collections;
using System;

public enum TouchDir {
    None,
    Left,
    Right,
    Top,
    Down,
}
public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 100;
    private TouchDir mTouchDir = TouchDir.None;
    private Vector3 mMouseDownPos;
    private ForestGenerator forestGenerator;
    private Transform prisoner;
    private int laneIndex = 1;// 0 1 2
    private float laneWidth = 14;
    private float laneOffset = 0;
    private float[] laneWayPointsOffset = { -14,0,14};
    private PlayerAnimation playerAnimation;
    public bool mIsSliding = false;
    private float mSlidingTime = 0;

    public bool mIsJump = false;
    private bool mIsJumpUp = false;
    public int mJumpSpeed = 30;
    public float mJumpHeight = 25;
    private float mHasJumpHeight = 0;
	// Use this for initialization
	void Start () {
        forestGenerator = Camera.main.GetComponent<ForestGenerator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        prisoner = transform.Find("Prisoner");
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.mGameState == GameState.Play) {
            MoveControl();
            Vector3 targetPos = forestGenerator.foreast1.GetComponent<Forest>().GetNextTargetPoint();
            targetPos.x += laneWayPointsOffset[laneIndex];
            Vector3 moveDir =  targetPos - transform.position;
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
            
            transform.LookAt(targetPos);
            //Camera.main.transform.LookAt(transform.position);

            //transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime);



        }
    }

    private void MoveControl()
    {
        TouchDir dir = GetTouchDir();
        Debug.Log(laneIndex);
        if (dir == TouchDir.Right || dir == TouchDir.Left) {
            Vector3 v = transform.position;
            v.x+= laneOffset;
        }

        if (mIsSliding)
        {
            mSlidingTime += Time.deltaTime;
            if (mSlidingTime > 1.40f)
            {
                mSlidingTime = 0;
                mIsSliding = false;
            }
        }
        if (mIsJump) {
            if (mIsJumpUp)
            {
                mHasJumpHeight += mJumpSpeed * Time.deltaTime;
                if (mJumpHeight - mHasJumpHeight < 0.5) {
                    mHasJumpHeight = mJumpHeight;
                    mIsJumpUp = false;
                }
                prisoner.position = new Vector3(prisoner.position.x,  mHasJumpHeight, prisoner.position.z);
            }
            else {
                mHasJumpHeight -= mJumpSpeed * Time.deltaTime;
                if (mHasJumpHeight < 8.9) {
                    mHasJumpHeight = 8.5f;
                    mIsJump = false;
                }
                prisoner.position = new Vector3(prisoner.position.x, mHasJumpHeight, prisoner.position.z);
            }
        }

    }

    private TouchDir GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0)) {
            mMouseDownPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) {
            Vector3 mouseUpPos = Input.mousePosition;
            Vector3 offset = mouseUpPos - mMouseDownPos;
            if (Mathf.Abs(offset.x) < 50 && Mathf.Abs(offset.y)<50) {
                laneOffset = 0;
                return TouchDir.None;
            }
            //Horizonal
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y) )
            {
                if (offset.x >0) {
                    laneOffset = laneWidth;
                    if (laneIndex == 2) {
                        laneOffset = 0;
                        return TouchDir.None;
                    }
                    ++laneIndex;
                    //playerAnimation.GetComponent<Animation>()["right"].speed =2 ;
                    //playerAnimation.PlayAnimation("right");
                    return TouchDir.Right;
                }
                laneOffset = -laneWidth;
                if (laneIndex == 0 ) {
                    laneOffset = 0;
                    return TouchDir.None;
                }
                --laneIndex;
                //playerAnimation.GetComponent<Animation>()["left"].speed = 2;
                //playerAnimation.PlayAnimation("left");
                return TouchDir.Left;
            }
            else {
                //Vertical
                if (offset.y > 0)
                {
                    if (!mIsJump) {
                        mIsJump = true;
                        mIsJumpUp = true;
                    }
                    return TouchDir.Top;
                }
                mIsSliding = true;
                return TouchDir.Down;
            }
        }
        laneOffset = 0;
        return TouchDir.None;
    }
}
