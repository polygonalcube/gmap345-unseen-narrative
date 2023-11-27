using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    public HPComponent hp;
    public HurtComponent hbox;
    public MoveComponent mover;
    public ShootingComponent shooter;

    public CharacterController charCon;

    public Vector3 movValue = Vector3.zero;

    public InputAction movement;

    public InputAction reflect;
    public bool isReflecting;
    int bulletCount;
    List<float> angles = new List<float>();

    public InputAction totemPower;
    public bool useTotem;
    public bool isSlowing;
    public bool totemActive = true;
    public float totemCooldown = 6f;
    public GameObject[] bullets;
    public List<string> totemsHeld = new List<string>();
    public bool canDash = true;
    public bool isDashing;
    public float dashPower = 40f;
    public float dashTime = 0.2f;
    public float dashCooldown = 3f;
    

    public GameObject camZone;

    public ParticleSystem dashParticles;

    void OnEnable()
    {
        movement.Enable();
        reflect.Enable();
        totemPower.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        reflect.Disable();
        totemPower.Disable();
    }

    void Start()
    {
        var em = dashParticles.emission;
        em.enabled = false;
        bulletCount = 0;
        
    }

    void Update()
    {
        ReceiveInput();
        if (Input.GetKeyDown(KeyCode.LeftShift) && (canDash == true))
        {
            StartCoroutine(Dash());
        }
        else
        {
            Movement();
        }
        Reflect();
        TotemPower();
        SlowTime();
        
        //WhenDying();
    }

    void FixedUpdate()
    {
        Fall();
    }

    void Movement()
    {
        mover.Move(movValue);
        mover.ResetY();
    }

    void ReceiveInput()
    {
        movValue = movement.ReadValue<Vector2>();
        movValue = new Vector3(movValue.x, 0f, movValue.y);

        isReflecting = (reflect.ReadValue<float>() == 1f);
        useTotem = (totemPower.ReadValue<float>() == 1f);
    }

    void Reflect()
    {
        if (isReflecting)
        {
            hbox.active = false;
        }
        else if (!isReflecting)
        {
            hbox.active = true;
            if (bulletCount > 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    shooter.Shoot(transform.position, positionOffset: Vector3.zero, 
                    shotDirection: new Vector3(0f, angles[i], 0f), targetPosition: transform.position, "Player Damage");
                }
            }
            bulletCount = 0;
            angles.Clear();
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

        if (isSlowing)
        {
            GameManager.gm.timeSlowMulti = GameManager.gm.timeSlowMultiSet;
            StartCoroutine(TotemCooldown());
        }
        
    }

    void TotemPower()
    {
        if (useTotem && totemActive == true)
        {
            if (totemsHeld.Contains("ward") == true)
            {
                Debug.Log("gone");
                StartCoroutine(TotemCooldown());

            }

        }
        
    }

    /*
    void WhenDying()
    {
        if (hp.health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
    */

    private IEnumerator Dash()
    {

        float startTime = Time.time;
        while (Time.time < startTime + dashTime) 
        {
            isDashing = true;
            canDash = false;
            transform.Translate(movValue * dashPower * Time.deltaTime);
            var em = dashParticles.emission;
            var dur = dashParticles.duration;
            em.enabled = true;
            Invoke(nameof(DisableDashDust), dur);
            isDashing = false;
            yield return null;
        }
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
        if (isReflecting)
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

}
