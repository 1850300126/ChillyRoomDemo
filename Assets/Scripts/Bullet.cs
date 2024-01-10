using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float bullet_speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PoolSystem.instance.PushGameObject(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
