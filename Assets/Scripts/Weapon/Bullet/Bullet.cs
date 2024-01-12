using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float bullet_speed;

    public GameObject bulletBoom;

    public GameObject BoomSmoke;

    private void Start()
    {
        PoolSystem.instance.AddPool("bulletBoom", bulletBoom, 50);
        PoolSystem.instance.AddPool("BoomSmoke", BoomSmoke, 50);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {   
            PoolSystem.instance.PushFromPool("bulletBoom", this.transform.position, Quaternion.identity);
            
            this.gameObject.SetActive(false);
        }

        IBeAttack _target = collision.gameObject.GetComponent<IBeAttack>();

        if (_target != null && _target.AttackType == AttackType.Enemy &&_target.DamageEnable == true)
        {
            _target.BeHit();

            CriticalHit();

            this.gameObject.SetActive(false);
        }
    }

    public void CriticalHit()
    {
        GameObject bigbang = 
            PoolSystem.instance.PushFromPool("bulletBoom", this.transform.position, Quaternion.identity);

        float rate = Random.Range(0f, 1f);

        if(rate > 0.5f)
        {
            bigbang.transform.localScale = new Vector3(5, 5, 1);

            GameObject smoke = PoolSystem.instance.PushFromPool("BoomSmoke", this.transform.position, Quaternion.identity);

            smoke.GetComponentInChildren<SpriteRenderer>().DOColor(new Vector4(0, 0, 0, 0), 5f);
        }
        else
        {
            bigbang.transform.localScale = Vector3.one;
        }
    }
}
