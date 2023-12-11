using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBigBoss : EnemyBaseClass
{
    public HPComponent hp;
    //public MoveComponent mover;
    public ShootingComponent[] shooters;

    public float spinAngle = 0f;
    public float spinSpd = 10f;

    public int nextAtk = 3;

    public float[] stateTimes = new float[3];

    public float stateTimer = 0f;

    public enum States
    {
        IDLE, //3
        CIRCLE, //6
        SPIN, //6
        FAST, //6
        DEATH //3
    }
    public States state = States.IDLE;
    
    public override void Start()
    {
        base.Start();
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
                        CheckHealth();
                        if (stateTimer > stateTimes[0])
                        {
                            stateTimer = 0f;
                            nextAtk++;
                            if (nextAtk > 3)
                            {
                                nextAtk = 1;
                            }
                            if (nextAtk == 1)
                                state = States.CIRCLE;
                            if (nextAtk == 2)
                                state = States.SPIN;
                            if (nextAtk == 3)
                                state = States.FAST;
                        }
                        break;
                    case States.CIRCLE:
                        CheckHealth();
                        if (stateTimer > stateTimes[1])
                        {
                            stateTimer = 0f;
                            state = States.IDLE;
                        }
                        shooters[0].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                        new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
                        shooters[1].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                        new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
                        break;
                    case States.SPIN:
                        CheckHealth();
                        if (stateTimer > stateTimes[2])
                        {
                            stateTimer = 0f;
                            state = States.IDLE;
                        }
                        spinAngle += Time.deltaTime * spinSpd;

                        shooters[2].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, spinAngle, 0f), targetPosition: player.transform.position);
                        shooters[3].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
                        new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
                        break;
                    case States.FAST:
                        CheckHealth();
                        if (stateTimer > stateTimes[3])
                        {
                            stateTimer = 0f;
                            state = States.IDLE;
                        }
                        shooters[4].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, 0f, 0f), targetPosition: player.transform.position);
                        shooters[5].Shoot(transform.position, positionOffset: Vector3.zero, 
                        shotDirection: new Vector3(0f, 0f, 0f), targetPosition: player.transform.position);
                        break;
                    case States.DEATH:
                        if (stateTimer > stateTimes[4])
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        break;
                }
            }
        }
    }

    float GetMoveAng()
    {
        return AngleToObject(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));
    }

    void CheckHealth()
    {
        if (hp.health <= 0)
            state = States.DEATH;
    }
}
