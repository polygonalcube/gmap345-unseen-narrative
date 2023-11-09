using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBaseClass
{
    //public MoveComponent mover;
    public ShootingComponent[] shooters;

    public Transform[] hands;
    Animator anim;

    public float[] stateTimes = new float[6];

    public Vector2 idealDist = new Vector2(3f, 5f);
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
            stateTimer += Time.deltaTime;
            switch(state)
            {
                case States.IDLE:
                    anim.Play("Idle");
                    if (stateTimer > stateTimes[0])
                    {
                        stateTimer = 0f;
                        state = States.CROSSFIRE;
                    }
                    break;
                case States.CROSSFIRE:
                    anim.Play("Crossfire");
                    if (stateTimer > stateTimes[2])
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
                    anim.Play("Idle");
                    if (stateTimer > stateTimes[4])
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
                    if (stateTimer > stateTimes[5])
                    {
                        stateTimer = 0f;
                        state = States.DEATH;
                    }
                    break;
            }
        }
    }
}
