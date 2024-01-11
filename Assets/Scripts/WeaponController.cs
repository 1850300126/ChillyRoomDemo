using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float shoot_speed;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public GameObject muzzle_fire;

    private string BulletName;

    private float shoot_timer = 0;

    private void Start()
    {   
        for (int i = 0; i < 50; i++)
        {
            GameObject _bullet = GetBullet();
            _bullet.SetActive(false);  
            PoolSystem.instance.PushGameObject(_bullet);
        }

        muzzle_fire.SetActive(false);
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
            Debug.Log("����");
            Shoot();
        }
    }

    private void Shoot()
    {
        MuzzleFire();

        MuzzleRandomRotate();

        GameObject _bulletObject = PoolSystem.instance.GetGameObject(BulletName);
        _bulletObject.transform.position = muzzle.position;
        _bulletObject.transform.rotation = muzzle.rotation;

        Bullet _bullet = _bulletObject.GetComponent<Bullet>();
            
        Vector2 _muzzleTrans = new Vector2(muzzle.right.x, muzzle.right.y);  

        _bullet.rb2D.velocity = _muzzleTrans * _bullet.bullet_speed;
    }

    private GameObject GetBullet()
    {
        GameObject _obj = Instantiate(bulletPrefab);

        BulletName = _obj.name;

        return _obj;

    }

    private void MuzzleRandomRotate()
    {
        float _randomRotate = Random.Range(-5f, 5f);

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
}
