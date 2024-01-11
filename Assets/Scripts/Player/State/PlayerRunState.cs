using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerStateMachine.playerController.myAnimator.CrossFade("Run", 0.1f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Move();

        CheckIsOnGround();
    }
}