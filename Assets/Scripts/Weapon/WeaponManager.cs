using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    public string weaponName;
    public WeaponBase weapon;
}


public class WeaponManager : MonoBehaviour
{   
    public List<WeaponInfo> weapons = new List<WeaponInfo>();

    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }

    public WeaponBase GetWeaponFromName(PlayerContro holder, string name)
    {   

        if(holder.current_weapon != null)
        {
            DestroyCurrentWeapon(holder.current_weapon);
        }


        foreach (var _weapon in weapons)
        {
            _weapon.weaponName = name;

            WeaponBase _targetWeapon = Instantiate(_weapon.weapon, holder.myAnimator.transform);

            _targetWeapon.Init(holder);

            return _weapon.weapon;
        }

        return null;
    }

    public void DestroyCurrentWeapon(WeaponBase weapon)
    {
        Destroy(weapon.gameObject);
    }
}
