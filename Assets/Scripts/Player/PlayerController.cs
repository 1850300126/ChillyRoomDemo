using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public float runSpeed;
    public float jumpSpeed;
    public bool canDoubleJump;
    public bool canFlip;

    public WeaponController currentWeapon;

    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private BoxCollider2D playerFeet;
    private bool mIsOnGround = true;
    private int jumpTimes = 0;



    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsOnGround();
        LockDirection();
        Run();
        Jump();
        Fall();
        Fire();
    }

    private void LockDirection()
    {
        if (Input.GetMouseButton(1))
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }

    private void CheckIsOnGround()
    {
        mIsOnGround = playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void Run()
    {
        var moveDir = Input.GetAxis("Horizontal");
        var playerVel = new Vector2(moveDir * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVel;
        var playerHasXSpeed = Math.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasXSpeed)
        {
            myAnimator.SetBool("isRunning", true);
            Flip();
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        if (!canFlip) return;

        if (myRigidBody2D.velocity.x > 0.1f)
        {
            myAnimator.transform.localRotation = Quaternion.Euler(0, 0, 0);
            CamaraController.Instance.SwitchFollowPoint(true);
        }
        if (myRigidBody2D.velocity.x < -0.1f)
        {
            myAnimator.transform.localRotation = Quaternion.Euler(0, 180, 0);
            CamaraController.Instance.SwitchFollowPoint(false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (mIsOnGround || (canDoubleJump && jumpTimes < 1))
            {
                var playerVel = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
                myRigidBody2D.velocity = playerVel;
                myAnimator.SetBool("isJumping", true);
                jumpTimes++;
            }
        }
    }

    private void Fall()
    {
        if (myRigidBody2D.velocity.y < -0.1f)
        {
            myAnimator.SetBool("isJumping", false);
        }

        if (mIsOnGround)
        {
            jumpTimes = 0;
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            currentWeapon.Fire();
        }
    }
}


