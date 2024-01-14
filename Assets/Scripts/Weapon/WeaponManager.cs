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
    // Å×¿ÇÔ¤ÖÆ
    public GameObject throwEggshell;
    // ÎäÆ÷Ô¤ÖÆ
    public List<WeaponInfo> weapons = new List<WeaponInfo>();

    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }


    public void OnLoaded()
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

                _targetWeapon.transform.parent = holder.weaponPoint.transform;

                _targetWeapon.transform.localPosition = Vector3.zero;

                _targetWeapon.transform.localRotation = Quaternion.identity;

                return _targetWeapon;
            }
        }
        return null;
    }

    public void PushPool()
    {
        PoolSystem.instance.AddPool("bullet", bulletPrefab, 50);

        PoolSystem.instance.AddPool("eggShell", throwEggshell, 50);
    }
    public void DestroyCurrentWeapon(WeaponBase weapon)
    {
        Destroy(weapon.gameObject);
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
