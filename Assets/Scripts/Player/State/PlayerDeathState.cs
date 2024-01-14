using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerStateBase
{
    public PlayerDeathState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void OnEnter()
    {
        playerStateMachine.playerController.myAnimator.CrossFade("Death", 0.1f);

        PlayerDeath();
    }

    public override void OnExit()
    {
        
    }

    public void PlayerDeath()
    {
        playerStateMachine.playerController.camaraController.DeathCamera();

        PlayerBack();
    }
    public void PlayerBack()
    {
        if (playerStateMachine.playerController.myAnimator.transform.localRotation != Quaternion.identity)
        {
            // 在右方
            playerStateMachine.playerController.myRigidBody2D.velocity = new Vector3(1, 5, 0);
        }
        else
        {
            // 人物在左方
            playerStateMachine.playerController.myRigidBody2D.velocity = new Vector3(-1, 5, 0);
        }

    }
}
