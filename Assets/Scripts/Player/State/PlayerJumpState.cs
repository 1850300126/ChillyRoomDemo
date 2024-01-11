using System;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerStateMachine.playerController.myAnimator.CrossFade("Jump", 0.1f);

        Jump();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        CheckIsOnGround();

        Move();

        OnFall();
    }

    private void OnFall()
    {
        if (playerStateMachine.playerController.myRigidBody2D.velocity.y < -0.1f)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerFallState);
        }

        if (playerStateMachine.playerController.mIsOnGround)
        {
            playerStateMachine.playerController.jumpTimes = 0;
        }
    }

    protected void Jump()
    {
        var playerVel = new Vector2(playerStateMachine.playerController.myRigidBody2D.velocity.x, playerStateMachine.playerController.jumpSpeed);
        playerStateMachine.playerController.myRigidBody2D.velocity = playerVel;
        playerStateMachine.playerController.jumpTimes++;
    }

    protected override void Move()
    {
        var playerVel = new Vector2(playerStateMachine.SharedData.playerInputValue.x * playerStateMachine.playerController.runSpeed, playerStateMachine.playerController.myRigidBody2D.velocity.y);

        playerStateMachine.playerController.myRigidBody2D.velocity = playerVel;

        Flip();
    }

    protected override void CheckIsOnGround()
    {
        playerStateMachine.playerController.mIsOnGround = playerStateMachine.playerController.playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}