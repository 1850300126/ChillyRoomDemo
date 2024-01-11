public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerStateMachine.playerController.myAnimator.CrossFade("Idle", 0.1f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        OnMove();

        CheckIsOnGround();
    }
}