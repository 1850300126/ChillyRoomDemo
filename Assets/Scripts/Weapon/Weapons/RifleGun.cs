using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RifleGun : WeaponBase
{   
    // 持有者
    public PlayerContro holder;

    // 射击速度（每秒
    public float shootSpeed;
    // 枪口位置
    public Transform muzzle;
    // 枪口火焰预制
    public GameObject muzzle_fire;
    // 抛壳位置
    public Transform throwEggshellPoint;
    private float shoot_timer = 0;
    public override void Init(PlayerContro holder)
    {   
        this.holder = holder;
        
        muzzle_fire.SetActive(false);
    }
    public override void Fire()
    {   
        RepeatingRifle();
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
        holder.camaraController.ShakeCamera(0.1f, 1f, 2f);
        holder.PlayerBack(0.1f);

        // 枪口火焰
        MuzzleFire();
        // 枪口乱转
        MuzzleRandomRotate();
        // 抛壳
        ThrowEggshell();

        // 发射子弹
        ShootBullet();

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
        // GameObject eggshell = Instantiate(throwEggshell, throwEggshellPoint.transform.position, throwEggshell.transform.rotation);
        GameObject eggshell = PoolSystem.instance.PushFromPoolAndDistoryByTime("eggShell", throwEggshellPoint.transform.position, throwEggshellPoint.transform.rotation, 5f);

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
        GameObject _bulletObject = PoolSystem.instance.PushFromPoolAndDistoryByTime("bullet", muzzle.position, muzzle.rotation, 10f);

        Bullet _bullet = _bulletObject.GetComponent<Bullet>();

        Vector2 _muzzleTrans = new Vector2(muzzle.right.x, muzzle.right.y);

        _bullet.rb2D.velocity = _muzzleTrans * _bullet.bullet_speed;
    }
}
