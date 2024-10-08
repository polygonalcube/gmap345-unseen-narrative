using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour, IDataPersistence
{
    public HPComponent hp;
    public HurtComponent hbox;
    public MoveComponent mover;
    public MoveComponent dashMover;
    public ShootingComponent shooter;

    public CharacterController charCon;

    //movement
    public InputAction movement;

    public Vector3 movValue = Vector3.zero;
    Vector3 prevMovValue = Vector3.right; //previous movValue != Vector3.zero for dash usage

    //dash
    public InputAction dash;

    public bool isDashing;

    float dashTimer;
    public float dashTimerSet = 0.2f;

    public float dashDelay;
    public float dashDelaySet = 3f;

    public AudioSource dashSound;

    //reflect
    public InputAction reflect;

    public bool isReflecting;
    int bulletCount;
    List<float> angles = new List<float>();

    public float reflectTime;
    public float reflectTimeMax = 3f;

    public float reflectDelay;
    public float reflectDelaySet = 5f;

    public int lastActiveScene;

    //totem use
    public InputAction totemPower;
    public bool useTotem;
    public bool isSlowing;
    public float timeSlowTime;
    public float timeSlowTimeSet = 4f;
    public bool totemActive = true;
    public float totemCooldown = 6f;
    public GameObject[] bullets;
    public List<string> totemsHeld = new List<string>();    

    public GameObject camZone;

    public ParticleSystem dashParticles;

    public enum States
    {
        IDLE,
        MOVE,
        DASH,
        DEATH
    }
    public States state = States.IDLE;

    void OnEnable()
    {
        movement.Enable();
        dash.Enable();
        reflect.Enable();
        totemPower.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        dash.Disable();
        reflect.Disable();
        totemPower.Disable();
    }

    void Start()
    {
        var em = dashParticles.emission;
        em.enabled = false;
        bulletCount = 0;
        Time.timeScale = 1f; //necessary to prevent the game from freezing in Time.timeScale = 0
        lastActiveScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        switch(state)
        {
            case States.IDLE:
                ReceiveInput();
                //Movement();
                Reflect();
                Fall();
                TotemPower();
                SlowTime();
                if (movValue != Vector3.zero)
                {
                    state = States.MOVE;
                }
                dashDelay -= Time.deltaTime;
                if (isDashing && dashDelay <= 0f)
                {
                    hbox.active = false;
                    dashTimer = dashTimerSet;
                    DashParticles();
                   state = States.DASH;
                }
                break;
            case States.MOVE:
                ReceiveInput();
                Movement();
                Reflect();
                Fall();
                TotemPower();
                SlowTime();
                if (movValue == Vector3.zero)
                {
                    state = States.IDLE;
                }
                dashDelay -= Time.deltaTime;
                if (isDashing && dashDelay <= 0f)
                {
                    hbox.active = false;
                    dashTimer = dashTimerSet;
                    DashParticles();
                    state = States.DASH;
                }
                break;
            case States.DASH:
                Dash();
                if (dashTimer <= 0f)
                {
                    hbox.active = true;
                    dashDelay = dashDelaySet;
                    DisableDashDust();
                    state = States.IDLE;
                }
                break;
        }
    }

    void ReceiveInput()
    {
        movValue = movement.ReadValue<Vector2>();
        movValue = new Vector3(movValue.x, 0f, movValue.y);
        if (movValue != Vector3.zero)
        {
            prevMovValue = movValue;
            Debug.Log(prevMovValue);
        }
        isDashing = (dash.ReadValue<float>() == 1f);
        isReflecting = (reflect.ReadValue<float>() == 1f);
        useTotem = (totemPower.ReadValue<float>() == 1f);
    }

    void Movement()
    {
        mover.Move(movValue);
        mover.ResetY();
    }

    void Dash()
    {
        dashMover.Move(prevMovValue);
        dashMover.ResetY();
        dashTimer -= Time.deltaTime;
    }

    void DashParticles()
    {
        //isDashing = true;
        //canDash = false;
        dashSound.Play();
        //transform.Translate(movValue * dashPower * Time.deltaTime);
        var em = dashParticles.emission;
        var dur = dashParticles.duration;
        em.enabled = true;
        Invoke(nameof(DisableDashDust), dur);
        //isDashing = false;
        //yield return null;
        //yield return new WaitForSeconds(dashCooldown);
        //canDash = true;
    }

    void Reflect()
    {
        reflectDelay -= Time.deltaTime;
        if (isReflecting && reflectDelay <= 0)
        {
            hbox.active = false;
            reflectTime += Time.deltaTime;
            if (reflectTime > reflectTimeMax)
            {
                reflectDelay = reflectDelaySet;
                isReflecting = false;
            }
        }
        else/* if (!isReflecting)*/
        {
            hbox.active = true;
            if (bulletCount > 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    shooter.Shoot(transform.position, positionOffset: Vector3.zero, 
                    shotDirection: new Vector3(0f, angles[i], 0f), targetPosition: transform.position, "Player Damage");
                    if (i >= 10)
                    {
                        break;
                    }
                }
                reflectDelay = reflectDelaySet;
            }
            bulletCount = 0;
            angles.Clear();
            reflectTime = 0f;
        }
    }

    void Fall()
    {
        if (!Physics.Raycast(transform.position + (Vector3.up * .1f), Vector3.down))
        {
            hp.health = 0;
        }
    }

    void SlowTime()
    {
        GameManager.gm.timeSlowMulti = 1f;

        if (isSlowing && timeSlowTime <= 0)
        {
            timeSlowTime = timeSlowTimeSet;
            //GameManager.gm.timeSlowMulti = GameManager.gm.timeSlowMultiSet;
            StartCoroutine(TotemCooldown());
            //StartCoroutine(SlowTimeForAmount());
        }
        if (timeSlowTime > 0f)
        {
            timeSlowTime -= Time.deltaTime;
            GameManager.gm.timeSlowMulti = GameManager.gm.timeSlowMultiSet;
        }
    }
    /*
    IEnumerator SlowTimeForAmount()
    {
        GameManager.gm.timeSlowMulti = GameManager.gm.timeSlowMultiSet;
        yield return new WaitForSeconds(4);
        GameManager.gm.timeSlowMulti = 1f;
    }
    */
    void TotemPower()
    {
        if (useTotem && totemActive == true)
        {
            if (totemsHeld.Contains("ward") == true)
            {
                WardClear();
                StartCoroutine(TotemCooldown());
            }
        }
    }

    IEnumerator TotemCooldown()
    {
        totemActive = false;
        yield return new WaitForSeconds(totemCooldown);
        totemActive = true;
    }

    void DisableDashDust()
    {
        var em = dashParticles.emission;
        em.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (isReflecting && reflectDelay <= 0)
        {
            bulletCount++;
            angles.Add(col.gameObject.transform.eulerAngles.y + 180f);
        }
        if (col.gameObject.tag == "Camera Zone")
        {
            camZone = col.gameObject;
        }
    }

    void WardClear()
    {
        bullets = GameObject.FindGameObjectsWithTag("Enemy Damage");

        foreach (GameObject bullets in bullets)
        {
            bullets.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        this.totemsHeld = data.totemsHeld;
        this.lastActiveScene = data.lastScene;
    }

    public void SaveData(ref GameData data)
    {
        data.totemsHeld = this.totemsHeld;
        data.lastScene = this.lastActiveScene;
    }
}
