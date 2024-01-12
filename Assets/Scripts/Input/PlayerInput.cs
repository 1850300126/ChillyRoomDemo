using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction playerInputAciton;

    public PlayerInputAction.MovementActions movementActions;

    public PlayerInputAction.SwitchWeaponActions switchWeaponActions;
    private void Awake()
    {
        playerInputAciton = new PlayerInputAction();

        movementActions = playerInputAciton.Movement;

        switchWeaponActions = playerInputAciton.SwitchWeapon;
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
