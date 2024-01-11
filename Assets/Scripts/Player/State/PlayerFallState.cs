using UnityEngine;

public class PlayerFallState : PlayerStateBase
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerStateMachine.playerController.myAnimator.CrossFade("Jump", 0.1f);


    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Move();

        CheckIsOnGround();

        OnGrouned();
    }

    protected override void CheckIsOnGround()
    {
        playerStateMachine.playerController.mIsOnGround = playerStateMachine.playerController.playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public void OnGrouned()
    {
        if (!playerStateMachine.playerController.mIsOnGround) return;

        if(playerStateMachine.SharedData.playerInputValue.x == 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
        }
        else
        {
            playerStateMachine.ChangeState(playerStateMachine.playerRunState);
        }
    }
    protected override void Move()
    {
        var playerVel = new Vector2(playerStateMachine.SharedData.playerInputValue.x * playerStateMachine.playerController.runSpeed, playerStateMachine.playerController.myRigidBody2D.velocity.y);

        playerStateMachine.playerController.myRigidBody2D.velocity = playerVel;

        Flip();
    }
}