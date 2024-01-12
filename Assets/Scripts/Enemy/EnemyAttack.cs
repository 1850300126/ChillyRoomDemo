using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBeAttack damageTarget = collision.gameObject.GetComponent<IBeAttack>();

        if (damageTarget != null && damageTarget.AttackType == AttackType.Player && damageTarget.DamageEnable == true)
        {
            damageTarget.BeHit();

            if(this.gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().transform.position -= new Vector3(0.1f, 0, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().transform.position += new Vector3(0.1f, 0, 0);
            }
        }
    }
}
