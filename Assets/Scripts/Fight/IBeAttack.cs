using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Player,
    Enemy
}


public interface IBeAttack
{
    public AttackType AttackType { get;}
    public bool DamageEnable { get; }
    public void BeHit();
}
