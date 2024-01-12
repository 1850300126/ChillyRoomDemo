using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachine
{
    // 状态间共享数据
    public PlayerSharedData SharedData { get; }
    public PlayerContro playerController { get; }
    public PlayerIdleState playerIdleState { get; }
    public PlayerJumpState playerJumpState { get; }
    public PlayerRunState playerRunState { get; }
    public PlayerFallState playerFallState { get; }
    public PlayerDeathState playerDeathState { get; }

    public PlayerStateMachine(PlayerContro playerController)
    {
        this.playerController = playerController;

        SharedData = new PlayerSharedData();

        playerIdleState = new PlayerIdleState(this);   

        playerJumpState = new PlayerJumpState(this);

        playerRunState = new PlayerRunState(this);

        playerFallState = new PlayerFallState(this);

        playerDeathState = new PlayerDeathState(this);
    }

}
