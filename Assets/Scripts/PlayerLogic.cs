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
    public MoveComponent mov;

    public Vector3 movValue = Vector3.zero;

    public InputAction movement;

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
        Movement();
        //WhenDying();
    }

    void FixedUpdate()
    {
        
    }

    void Movement()
    {
        mov.Move(movValue);
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
}
