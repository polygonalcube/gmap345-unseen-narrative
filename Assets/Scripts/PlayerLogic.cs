using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //TO-DO:
    //Make components project specific; don't try general
    //public HealthComponent hp;
    //public HurtboxComponent hbox;
    public MoveComponent mover;
    public CharacterController charCon;

    public Vector3 movValue = Vector3.zero;

    public InputAction movement;

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 40f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    void Start()
    {
        //eh?
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
}
