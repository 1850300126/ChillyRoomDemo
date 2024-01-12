
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class PlayerContro : MonoBehaviour, IBeAttack
{
    public float runSpeed;
    public float jumpSpeed;


    public bool canDoubleJump = true;
    public bool canFlip = true;
    public bool mIsOnGround = true;
    public bool fire = false;
    public int jumpTimes = 0;

    // 受伤时间
    public float lastBeHitTime = 0;
    // 无敌间隔
    public float NoHitTime = 2f;


    public Rigidbody2D myRigidBody2D;
    public Animator myAnimator;
    public BoxCollider2D playerFeet;
    public PlayerInput player_input;
    public CamaraController camaraController;



    public WeaponBase current_weapon;

    public Transform weaponPoint;
    public PlayerStateMachine playerStateMachine { get; set; }

    public AttackType attackType;
    public AttackType AttackType
    {
        get => attackType;
    }

    private float playerHP = 10;
    public float PlayerHP
    {
        get => playerHP;
        set
        {
            playerHP = value;
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

        current_weapon = WeaponManager.instance.GetWeaponFromName(this, "HandGun");

        AddInputAction();
    }

    private void OnDestroy()
    {
        RemoveInputAction();
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

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    #region 通用方法

    private void AddInputAction()
    {
        player_input.playerInputAciton.SwitchWeapon.SwitchFirstWeapon.started += SelectWeapon;
        player_input.playerInputAciton.SwitchWeapon.SwitchSecondWeapon.started += SelectWeapon;
        player_input.playerInputAciton.SwitchWeapon.SwitchThirdWeapon.started += SelectWeapon;
    }

    private void RemoveInputAction()
    {
        player_input.playerInputAciton.SwitchWeapon.SwitchFirstWeapon.started -= SelectWeapon;
        player_input.playerInputAciton.SwitchWeapon.SwitchSecondWeapon.started -= SelectWeapon;
        player_input.playerInputAciton.SwitchWeapon.SwitchThirdWeapon.started -= SelectWeapon;
    }
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

    public void PlayerBack(float frequency)
    {
        if (myAnimator.transform.localRotation != Quaternion.identity)
        {
            // 在右方
            myRigidBody2D.transform.position += new Vector3(frequency, 0, 0);
        }
        else
        {
            // 人物在左方
            myRigidBody2D.transform.position -= new Vector3(frequency, 0, 0);
        }

    }

    public void SelectWeapon(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.name);

        switch (context.action.name)
        {
            case "SwitchFirstWeapon":
                current_weapon = WeaponManager.instance.GetWeaponFromName(this, "RifleGun");
                GameUIManager.Instance.playerHPUI.UpdateSwitchWeaponUIText("切换为" + "<color=\"red\">断魄</color>");
                break;
            case "SwitchSecondWeapon":
                current_weapon = WeaponManager.instance.GetWeaponFromName(this, "ShotGun");
                GameUIManager.Instance.playerHPUI.UpdateSwitchWeaponUIText("切换为" + "<color=\"blue\">荧焰</color>");
                break;
            case "SwitchThirdWeapon":
                current_weapon = WeaponManager.instance.GetWeaponFromName(this, "HandGun");
                GameUIManager.Instance.playerHPUI.UpdateSwitchWeaponUIText("切换为" + "<color=\"purple\">坠明</color>");
                break;
            default:
                break;
        }

    }

    public void BeHit()
    {
        if (playerStateMachine.current_state == playerStateMachine.playerDeathState) return;

        if (Time.time - NoHitTime < lastBeHitTime) return;
        lastBeHitTime = Time.time;

        HPReduce();

        BeHitFlash(1);
        PlayerBack(0.1f);
        camaraController.ShakeCamera(0.2f, 3f, 3f);
    }
    private void HPReduce()
    {
        playerHP -= 2f;

        GameUIManager.Instance.playerHPUI.UpdateUI(playerHP / 10f);

        if (playerHP <= 0)
        {
            damageEnable = false;
            playerStateMachine.ChangeState(playerStateMachine.playerDeathState);
        }
    }
    private void BeHitFlash(int value)
    {
        Material material = myAnimator.GetComponent<SpriteRenderer>().material;

        material.SetInt("_BeAttack", value);

        IEnumerator resetShader = ResetShader();

        StartCoroutine(resetShader);
    }

    private IEnumerator ResetShader()
    {
        yield return new WaitForSeconds(0.2f);

        BeHitFlash(0);
    }

    #endregion
}

