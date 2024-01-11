using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction playerInputAciton;
    public PlayerInputAction.MovementActions movementActions;
    private void Awake()
    {
        playerInputAciton = new PlayerInputAction();

        movementActions = playerInputAciton.Movement;
    }
    private void OnEnable()
    {
        playerInputAciton.Enable();
    }
    private void OnDisable()
    {
        playerInputAciton.Disable();
    }

}
