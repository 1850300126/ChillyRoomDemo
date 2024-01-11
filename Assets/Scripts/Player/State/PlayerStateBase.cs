using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateBase : IState
{
    public PlayerStateMachine playerStateMachine;
    public PlayerStateBase(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;

    }

    public virtual void OnEnter()
    {
        Debug.Log("��ǰ����״̬��" + this);
        AddInputAction();
    }

    public virtual void OnHandle()
    {
        ReadMovementInput();
    }


    public virtual void OnExit()
    {
        RemoveInputAction();
    }

    public virtual void OnFixedUpdate()
    {
        
    }

    public virtual void OnTriggerEnter(Collider2D collider)
    {
       
    }

    public virtual void OnTriggerExit(Collider2D collider)
    {
        
    }

    public virtual void OnTriggerStay(Collider2D collider)
    {
        
    }

    public virtual void OnUpdate()
    {
        GetPlayerInput();
    }
    #region �����¼�

    protected virtual void AddInputAction()
    {
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Jump.started += OnJump;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Attack.started += OnAttackStart;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Attack.canceled += OnAttackCanceled;

        playerStateMachine.playerController.player_input.playerInputAciton.Movement.LockDirection.started += OnLockDirection;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.LockDirection.canceled += OnUnLockDirection;

    }
    protected virtual void RemoveInputAction()
    {
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Jump.started -= OnJump;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Attack.started -= OnAttackStart;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.Attack.canceled += OnAttackCanceled;


        playerStateMachine.playerController.player_input.playerInputAciton.Movement.LockDirection.started += OnLockDirection;
        playerStateMachine.playerController.player_input.playerInputAciton.Movement.LockDirection.canceled += OnUnLockDirection;
    }
    #endregion
    #region �����¼�
    // �õ��û�������
    private void ReadMovementInput()
    {
        playerStateMachine.SharedData.playerInputValue = playerStateMachine.playerController.player_input.playerInputAciton.Movement.Move.ReadValue<Vector2>();
    }
    protected void GetPlayerInput()
    {
        // var moveDir = Input.GetAxis("Horizontal");

        // playerStateMachine.SharedData.inputDirection = moveDir;
    }
    // ���ƶ�ʱ
    protected void OnMove()
    {
        if (playerStateMachine.SharedData.playerInputValue.x == 0) return;

        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }
    // ����Ծʱ
    protected void OnJump(InputAction.CallbackContext context)
    {
        if (playerStateMachine.playerController.mIsOnGround || (playerStateMachine.playerController.canDoubleJump && playerStateMachine.playerController.jumpTimes < 1))
        {
            playerStateMachine.ChangeState(playerStateMachine.playerJumpState);
        }
    }
    protected void OnAttackStart(InputAction.CallbackContext context)
    {
        playerStateMachine.playerController.fire = true;
    }
    protected void OnAttackCanceled(InputAction.CallbackContext context)
    {
        playerStateMachine.playerController.fire = false;
    }
    protected void OnLockDirection(InputAction.CallbackContext context)
    {
        playerStateMachine.playerController.canFlip = true;
    }
    protected void OnUnLockDirection(InputAction.CallbackContext context)
    {
        playerStateMachine.playerController.canFlip = false;
    }
    #endregion

    #region Main
    // �ƶ�
    protected virtual void Move()
    {
        if(playerStateMachine.SharedData.playerInputValue.x == 0)
             playerStateMachine.ChangeState(playerStateMachine.playerIdleState);

        var playerVel = new Vector2(playerStateMachine.SharedData.playerInputValue.x * playerStateMachine.playerController.runSpeed, playerStateMachine.playerController.myRigidBody2D.velocity.y);

        playerStateMachine.playerController.myRigidBody2D.velocity = playerVel;

        Flip();
    }
    // ���ӵ�
    protected virtual void CheckIsOnGround()
    {
        playerStateMachine.playerController.mIsOnGround = playerStateMachine.playerController.playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!playerStateMachine.playerController.mIsOnGround)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerFallState);
        }
    }
    // �ı䷽��
    protected void Flip()
    {
        if (!playerStateMachine.playerController.canFlip) return;

        if (playerStateMachine.playerController.myRigidBody2D.velocity.x > 0.1f)
        {
            playerStateMachine.playerController.myAnimator.transform.localRotation = Quaternion.Euler(0, 0, 0);
            playerStateMachine.playerController.camaraController.SwitchFollowPoint(true);
        }
        if (playerStateMachine.playerController.myRigidBody2D.velocity.x < -0.1f)
        {
            playerStateMachine.playerController.myAnimator.transform.localRotation = Quaternion.Euler(0, 180, 0);
            playerStateMachine.playerController.camaraController.SwitchFollowPoint(false);
        }
    }

    #endregion
}
