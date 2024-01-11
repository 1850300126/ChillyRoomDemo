using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContro : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public bool canDoubleJump;
    public bool canFlip;


    public Rigidbody2D myRigidBody2D;
    public Animator myAnimator;
    public BoxCollider2D playerFeet;
    public PlayerInput player_input;

    public bool mIsOnGround = true;
    public int jumpTimes = 0;

    public bool fire = false;

    public CamaraController camaraController;
    public WeaponBase current_weapon;

    public Transform weaponPoint;
    public PlayerStateMachine playerStateMachine { get; set; }
    private void Awake()
    {
        playerStateMachine = new PlayerStateMachine(this);

        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
        player_input = GetComponent<PlayerInput>();
        camaraController = GetComponent<CamaraController>();


    }
    // Start is called before the first frame update
    void Start()
    {
        Init();

        current_weapon = WeaponManager.instance.GetWeaponFromName(this, "RifleGun");
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        playerStateMachine.OnHandle();
        playerStateMachine.OnUpdate();   
    }

    private void FixedUpdate()
    {
        playerStateMachine.OnFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerStateMachine.OnTriggerEnter(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerStateMachine.OnTriggerStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerStateMachine.OnTriggerExit(collision);
    }

    #region 通用方法

    private void Init()
    {
        if (transform.transform.localEulerAngles == Vector3.zero)
        {
            camaraController.SwitchFollowPoint(true);
        }
        else
        {
            camaraController.SwitchFollowPoint(false);
        }

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }
    private void Fire()
    {
        if (!fire) return;
        current_weapon.Fire();
    }

    public void PlayerBack()
    {
        if (myAnimator.transform.localRotation != Quaternion.identity)
        {
            // 在右方
            myRigidBody2D.transform.position += new Vector3(0.05f, 0, 0);
        }
        else
        {
            // 人物在左方
            myRigidBody2D.transform.position -= new Vector3(0.05f, 0, 0);
        }

    }
    #endregion
}

