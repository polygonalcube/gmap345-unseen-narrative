using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBaseClass
{
    //public MoveComponent mover;
    public ShootingComponent[] shooters;
    Animator anim;

    public float idleTime = 0f;

    public Vector2 idealDist = new Vector2(3f, 5f);
    public float shootTime = 0f;
    public float stateTimer = 0f;

    public enum States
    {
        IDLE,
        SWIPE,
        CROSSFIRE,
        EXPLOSION,
        TIMEOUT,
        DEATH
    }
    public States state = States.TIMEOUT;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            switch(state)
            {
                case States.IDLE:
                    //anim.Play("Idle");
                    stateTimer += Time.deltaTime;
                    if (stateTimer > idleTime)
                    {
                        stateTimer = 0f;
                        state = States.TIMEOUT;
                    }
                    break;
                case States.TIMEOUT:
                    //anim.Play("Crossfire");
                    stateTimer += Time.deltaTime;
                    if (stateTimer > shootTime)
                    {
                        stateTimer = 0f;
                        state = States.IDLE;
                    }
                    else
                    {
                        for (int i = 0; i < shooters.Length; i++)
                        {
                            shooters[i].Shoot(transform.position, positionOffset: Vector3.zero, 
                            shotDirection: new Vector3(0f, (AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                            new Vector2(player.transform.position.x, player.transform.position.z))/* + Random.Range(-2f, 2f)*/), 0f), 
                            targetPosition: player.transform.position);
                        }
                    }
                    break;
            }
        }
    }
}
