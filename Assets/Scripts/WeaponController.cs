using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float shoot_speed;
    public Transform weapon_muzzle;
    public GameObject bulletPrefab;

    public string BulletName;

    private float shoot_timer = 0;

    private void Start()
    {   
        for (int i = 0; i < 50; i++)
        {
            GameObject _bullet = GetBullet();
            _bullet.SetActive(false);  
            PoolSystem.instance.PushGameObject(_bullet);
        }
    }

    public void Fire()
    {
        float shooting_interval;
        if (shoot_speed == 0)
        {
            shooting_interval = 10000;
        }
        else
        {
            shooting_interval = 1 / shoot_speed;
        }

        shoot_timer += Time.deltaTime;
        if (shoot_timer >= shooting_interval)
        {
            shoot_timer = 0;
            Debug.Log("·¢Éä");
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject _bulletObject = PoolSystem.instance.GetGameObject(BulletName);
        _bulletObject.transform.position = weapon_muzzle.position;
        _bulletObject.transform.rotation = weapon_muzzle.rotation;

        Bullet _bullet = _bulletObject.GetComponent<Bullet>();
            
        Vector2 _muzzleTrans = new Vector2(weapon_muzzle.right.x, weapon_muzzle.right.y);  

        _bullet.rb2D.velocity = _muzzleTrans * _bullet.bullet_speed;
    }

    private GameObject GetBullet()
    {
        GameObject _obj = Instantiate(bulletPrefab);

        BulletName = _obj.name;

        return _obj;

    }

    private void MuzzleRotate()
    {
           
    }
}
