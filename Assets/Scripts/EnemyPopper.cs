using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopper : EnemyBaseClass
{
    public MoveComponent mover;
    public ShootingComponent shooter;

    public float idleTime = 0f;

    public Vector2 idealDist = new Vector2(3f, 5f);
    public float shootTime = 0f;
    public float stateTimer = 0f;

    public enum States
    {
        IDLE,
        MOVE,
        SHOOT
    }
    public States state = States.MOVE;

    void Update()
    {
        if (player != null)
        {
            switch(state)
            {
                case States.IDLE:
                    stateTimer += Time.deltaTime;
                    if (stateTimer > idleTime)
                    {
                        stateTimer = 0f;
                        state = States.MOVE;
                    }
                    break;
                case States.MOVE:
                    if ((Vector3.Distance(transform.position, player.transform.position) < idealDist.x) || 
                    (Vector3.Distance(transform.position, player.transform.position) > idealDist.y))
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) > idealDist.y)
                        {
                            transform.eulerAngles = new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                            new Vector2(player.transform.position.x, player.transform.position.z)), 0f);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                            new Vector2(player.transform.position.x, player.transform.position.z)) + 180f, 0f);
                        }
                        mover.MoveAngularly(transform.forward * GameManager.gm.timeSlowMulti);
                        transform.eulerAngles = Vector3.zero;
                    }
                    else
                    {
                        stateTimer = 0f;
                        state = States.SHOOT;
                    }
                    mover.ResetY();
                    break;
                case States.SHOOT:
                    stateTimer += Time.deltaTime;
                    if (stateTimer > shootTime)
                    {
                        stateTimer = 0f;
                        state = States.IDLE;
                    }
                    else
                    {
                        shooter.Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                        new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
                    }
                    break;
            }
        }
    }   
}
