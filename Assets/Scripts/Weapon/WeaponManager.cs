using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    public string weaponName;
    public WeaponBase weapon;
}


public class WeaponManager : MonoBehaviour
{   
    // ×Óµ¯Ô¤ÖÆ
    public GameObject bulletPrefab;
    public List<WeaponInfo> weapons = new List<WeaponInfo>();

    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PushPool();
    }

    public WeaponBase GetWeaponFromName(PlayerContro holder, string name)
    {   

        if(holder.current_weapon != null)
        {
            DestroyCurrentWeapon(holder.current_weapon);
        }


        foreach (var _weapon in weapons)
        {
            if(_weapon.weaponName == name)
            {
                WeaponBase _targetWeapon = Instantiate(_weapon.weapon, holder.myAnimator.transform);

                _targetWeapon.Init(holder);

                return _targetWeapon;
            }
        }
        return null;
    }

    public void PushPool()
    {
        PoolSystem.instance.AddPool("bullet", bulletPrefab, 50);
    }
    public void DestroyCurrentWeapon(WeaponBase weapon)
    {
        Destroy(weapon.gameObject);
    }
}
