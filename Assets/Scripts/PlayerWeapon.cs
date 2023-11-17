using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public HitComponent hit;
    public InputAction attacking;
    public Vector3 atkValue = Vector3.zero;

    public Vector3 atkPos = Vector3.zero;

    Vector3 refVelocity = Vector3.zero;
    public float smoothTime = .5f;
    public float maxSpeed = Mathf.Infinity;

    public bool isAttacking = false;

    void OnEnable()
    {
        attacking.Enable();
    }

    void OnDisable()
    {
        attacking.Disable();
    }

    void Start()
    {
        hit.enabled = true;
    }

    void Update()
    {
        ReceiveInput();
        Attack();
    }

    void ReceiveInput()
    {
        atkValue = attacking.ReadValue<Vector2>();
        atkValue = new Vector3(atkValue.x, 0f, atkValue.y);
    }

    void Attack()
    {
        if (atkValue != Vector3.zero)
        {
            isAttacking = true;
            atkPos = atkValue * 2f;
        }
        else if (Vector3.Distance(transform.localPosition, atkPos) < 0.01f)
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            hit.enabled = true;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, atkPos, ref refVelocity, smoothTime, maxSpeed);
        }
        else
        {
            hit.enabled = false;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(-.8f, 0.2f, 0), ref refVelocity, smoothTime, maxSpeed);
        }
    }
}
