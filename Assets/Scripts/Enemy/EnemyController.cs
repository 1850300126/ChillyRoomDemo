using System.Collections;
using System.Collections.Generic;
using EasyUpdateDemoSDK;
using UnityEngine;
public enum EnemyState
{
    Live,
    Dead
}

public class EnemyController : MonoBehaviour, IBeAttack
{
    public Rigidbody2D rb2D;
    public Animator animator;
    public CapsuleCollider2D capsuleCollider;
    public BoxCollider2D damageCollider;
    public Transform targetPosition; // Ä¿±êÎ»ÖÃ
    public float speed = 5;
    public EnemyState enemyState;

    private int currentPointIndex = 0;

    public AttackType attackType;
    public AttackType AttackType 
    { 
        get => attackType;
    }

    private float enemyHP = 10;
    public float EnemyHP
    {
        get => enemyHP;
        set
        {
            enemyHP = value;
        }    
    }

    private bool damageEnable = true;
    public bool DamageEnable
    {
        get => damageEnable;
        private set
        {
            damageEnable = value;
        }
    }

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        damageCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        targetPosition = EnemyManager.instance.enemyMovePoints[currentPointIndex];
    }

    void FixedUpdate()
    {
        if (enemyState == EnemyState.Dead) return;

        MoveObject();
    }
    public void Init()
    {
        speed = Random.Range(3f, 6f);
    }

    void MoveObject()
    {
        Vector3 direction = targetPosition.position - transform.position;
        float distance = direction.magnitude; 

        if (distance > 1f)
        {
            direction.Normalize(); 

            rb2D.velocity = new Vector2(direction.x * speed, rb2D.velocity.y); 
        }
        else
        {
            if(currentPointIndex < EnemyManager.instance.enemyMovePoints.Count - 1)
            {
                currentPointIndex++;
            }
            else
            {
                currentPointIndex = 0;
            }

            targetPosition = EnemyManager.instance.enemyMovePoints[currentPointIndex];

            Rotate(targetPosition);
        }
    }

    void Rotate(Transform targetPos)
    {
        float dir = Vector2.Dot(new Vector2(0, this.transform.position.y), targetPos.position);
        if(dir > 0)
        {
            this.rb2D.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.rb2D.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void EnemyBack()
    {
        if (PlayerManager.instance.GetPlayerTransform().position.x < this.transform.position.x)
        {
            this.rb2D.transform.position += new Vector3(1f, 0, 0);
        }
        else
        {
            this.rb2D.transform.position -= new Vector3(1f, 0, 0);
        }
    }
    public void BeHit()
    {
        if (enemyState == EnemyState.Dead) return;

        EnemyBack();

        enemyHP -= 5;

        animator.SetTrigger("beHit");

        if (enemyHP <= 0)
        {
            enemyState = EnemyState.Dead;
            damageEnable = false;
            rb2D.velocity = Vector3.zero;
            damageCollider.enabled = false;
            animator.SetTrigger("isDead");

            Invoke(nameof(DestroySelf), 10f);

            MsgSystem.instance.SendMsg("EnemyDead", null);
        }
    }

    private void DestroySelf()
    {
       Destroy(gameObject);
    }
}
