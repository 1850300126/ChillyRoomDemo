using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{    // ������
    public PlayerContro holder;
    // ǹ��λ��
    public List<Transform> muzzles = new List<Transform>();
    // ǹ�ڻ���Ԥ��
    public GameObject muzzle_fire;
    // �׿�Ԥ��
    public GameObject throwEggshell;
    // �׿�λ��
    public Transform throwEggshellPoint;

    public override void Init(PlayerContro holder)
    {
        this.holder = holder;
    }
    public override void Fire()
    {
        ShotGunFire();
    }

    public void ShotGunFire()
    {
        Shoot();

        holder.fire = false;
    }

    private void Shoot()
    {   
        // ��������Ļ��
        holder.camaraController.ShakeCamera(0.2f, 2f, 2f);
        holder.PlayerBack();

        // ǹ�ڻ���
        MuzzleFire();
        // �׿�
        ThrowEggshell();
        // �����ӵ�
        ShootBullet();

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
        for(int i = 0; i < muzzles.Count; i++)
        {
            GameObject _bulletObject = PoolSystem.instance.PushFromPoolAndDistoryByTime("bullet", muzzles[i].position, muzzles[i].rotation, 10f);

            Bullet _bullet = _bulletObject.GetComponent<Bullet>();

            Vector2 _muzzleTrans = new Vector2(muzzles[i].right.x, muzzles[i].right.y);

            _bullet.rb2D.velocity = _muzzleTrans * _bullet.bullet_speed;
        }
    }
}
