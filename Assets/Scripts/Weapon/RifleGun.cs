using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGun : WeaponBase
{   
    // 持有者
    public PlayerContro holder;

    // 射击速度（每秒
    public float shootSpeed;
    // 枪口位置
    public Transform muzzle;
    // 子弹预制
    public GameObject bulletPrefab;
    // 枪口火焰预制
    public GameObject muzzle_fire;
    // 抛壳预制
    public GameObject throwEggshell;
    // 抛壳位置
    public Transform throwEggshellPoint;


    private string BulletName;
    private float shoot_timer = 0;
    public override void Init(PlayerContro holder)
    {
        this.holder = holder;

        PushPool();
    }
    public override void Fire()
    {
        RepeatingRifle();
    }
    public void PushPool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject _bullet = GetBullet();
            _bullet.SetActive(false);
            PoolSystem.instance.PushGameObject(_bullet);
        }
        muzzle_fire.SetActive(false);
    }

    public void RepeatingRifle()
    {
        float shooting_interval;
        if (shootSpeed == 0)
        {
            shooting_interval = 10000;
        }
        else
        {
            shooting_interval = 1 / shootSpeed;
        }

        shoot_timer += Time.deltaTime;
        if (shoot_timer >= shooting_interval)
        {
            shoot_timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {   
        // 控制者屏幕震动
        holder.camaraController.ShakeCamera(0.1f, 1f, 1f);
        holder.PlayerBack();

        // 枪口火焰
        MuzzleFire();
        // 枪口乱转
        MuzzleRandomRotate();
        // 抛壳
        ThrowEggshell();

        // 发射子弹
        ShootBullet();

    }
    private GameObject GetBullet()
    {
        GameObject _obj = Instantiate(bulletPrefab);

        BulletName = _obj.name;

        return _obj;

    }

    private void MuzzleRandomRotate()
    {
        float _randomRotate = UnityEngine.Random.Range(-5f, 5f);

        muzzle.localEulerAngles = new Vector3(0, 0, _randomRotate);
    }

    private void MuzzleFire()
    {
        IEnumerator playMuzzleFire = PlayMuzzleFire();

        StartCoroutine(playMuzzleFire);
    }

    private IEnumerator PlayMuzzleFire()
    {
        muzzle_fire.SetActive(true);
        yield return new WaitForSeconds(Time.deltaTime * 15);
        muzzle_fire.SetActive(false);
    }

    private void ThrowEggshell()
    {
        GameObject eggshell = Instantiate(throwEggshell, throwEggshellPoint.transform.position, throwEggshell.transform.rotation);

        Vector2 shootDirection;

        if (throwEggshellPoint.transform.eulerAngles.y == 0)
        {
            shootDirection = new Vector3(-0.5f, 0.5f);
        }
        else
        {
            shootDirection = new Vector3(0.5f, 0.5f);
        }

        eggshell.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f;
    }

    private void ShootBullet()
    { 
        GameObject _bulletObject = PoolSystem.instance.GetGameObject(BulletName);
        _bulletObject.transform.position = muzzle.position;
        _bulletObject.transform.rotation = muzzle.rotation;

        Bullet _bullet = _bulletObject.GetComponent<Bullet>();

        Vector2 _muzzleTrans = new Vector2(muzzle.right.x, muzzle.right.y);

        _bullet.rb2D.velocity = _muzzleTrans * _bullet.bullet_speed;
    }
}
