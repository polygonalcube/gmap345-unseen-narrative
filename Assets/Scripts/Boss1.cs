using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBaseClass
{
    public MoveComponent mover;
    public ShootingComponent[] shooters;

    public Transform[] hands;
    Animator anim;

    public float[] stateTimes = new float[7];

    public float swipeDist = 1f;
    public float idealDist = 6f;
    public float stateTimer = 0f;

    public enum States
    {
        IDLE, //1
        MOVE, //3
        SWIPE, //1
        CROSSFIRE, //2
        EXPLOSION, //5
        TIMEOUT, //30
        DEATH //3
    }
    public States state = States.IDLE;
    
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 35f)
            {
                stateTimer += Time.deltaTime;
                switch(state)
                {
                    case States.IDLE:
                        anim.Play("Idle");
                        if (stateTimer > stateTimes[0])
                        {
                            stateTimer = 0f;
                            if (CheckPlayerDistance())
                            {
                                state = States.SWIPE;
                            }
                            else
                            {
                                state = States.CROSSFIRE;
                            }
                        }
                        break;
                    case States.MOVE:
                        anim.Play("Idle");
                        if (stateTimer > stateTimes[1])
                        {
                            stateTimer = 0f;
                            state = States.CROSSFIRE;
                        }
                        transform.eulerAngles = new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                        new Vector2(player.transform.position.x, player.transform.position.z)) + 180f, 0f);
                        mover.MoveAngularly(transform.forward * GameManager.gm.timeSlowMulti);
                        mover.ResetY();
                        transform.eulerAngles = Vector3.zero;
                        break;
                    case States.SWIPE:
                        anim.Play("Swipe");
                        if (stateTimer > stateTimes[2])
                        {
                            stateTimer = 0f;
                            state = States.MOVE;
                        }
                        break;
                    case States.CROSSFIRE:
                        anim.Play("Crossfire");
                        if (stateTimer > stateTimes[3])
                        {
                            stateTimer = 0f;
                            if (CheckPlayerDistance())
                            {
                                state = States.SWIPE;
                            }
                            else if (Random.Range(0, 2) % 2 == 0)
                            {
                                state = States.IDLE;
                            }
                            else
                            {
                                state = States.MOVE;
                            }
                        }
                        for (int i = 0; i < shooters.Length; i++)
                        {
                            shooters[i].Shoot(hands[i].position, positionOffset: Vector3.zero, 
                            shotDirection: new Vector3(0f, hands[i].eulerAngles.y, 0f), 
                            targetPosition: player.transform.position);
                        }
                        break;
                    case States.EXPLOSION:
                        anim.Play("Explosion");
                        if (stateTimer > stateTimes[4])
                        {
                            stateTimer = 0f;
                            state = States.IDLE;
                        }
                        for (int i = 0; i < shooters.Length; i++)
                        {
                            shooters[i].Shoot(hands[i].position, positionOffset: Vector3.zero, 
                            shotDirection: new Vector3(0f, hands[i].eulerAngles.y + 180f, 0f), 
                            targetPosition: player.transform.position);
                        }
                        break;
                    case States.TIMEOUT:
                        anim.Play("Timeout");
                        if (stateTimer > stateTimes[5])
                        {
                            stateTimer = 0f;
                            state = States.DEATH;
                        }
                        for (int i = 0; i < shooters.Length; i++)
                        {
                            shooters[i].Shoot(hands[i].position, positionOffset: Vector3.zero, 
                            shotDirection: new Vector3(0f, (AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                            new Vector2(player.transform.position.x, player.transform.position.z)) + Random.Range(-15f, 15f)), 0f), 
                            targetPosition: player.transform.position);
                        }
                        break;
                    case States.DEATH:
                        anim.Play("Death");
                        if (stateTimer > stateTimes[6])
                        {
                            stateTimer = 0f;
                            state = States.DEATH;
                        }
                        break;
                }
            }
        }
    }

    bool CheckPlayerDistance()
    {
        return (Vector3.Distance(transform.position, player.transform.position) <= swipeDist);
    }
}
