using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float bullet_speed;

    public GameObject bulletBoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {   
            Instantiate(bulletBoom, this.transform.position, Quaternion.identity);

            PoolSystem.instance.PushGameObject(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
