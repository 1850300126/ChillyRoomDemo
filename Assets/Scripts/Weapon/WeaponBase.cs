using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public abstract void Init(PlayerContro holder);
    public abstract void Fire();
}
