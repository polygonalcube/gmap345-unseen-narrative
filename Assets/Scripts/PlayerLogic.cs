using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 40f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    void OnEnable()
    {
        movement.Enable();
        reflect.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        reflect.Disable();
    }

    void Start()
    {
        bulletCount = 0;
    }

    void Update()
    {
        ReceiveInput();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
        else
        {
            Movement();
        }
        Reflect();
        //WhenDying();
    }

    void FixedUpdate()
    {
        
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
            transform.Translate(movValue * dashPower * Time.deltaTime);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (isReflecting)
        {
            bulletCount++;
            angles.Add(col.gameObject.transform.eulerAngles.y + 180f);
        }
    }
}
