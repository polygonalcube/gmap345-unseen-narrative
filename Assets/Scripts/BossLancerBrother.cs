using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLancerBrother : EnemyBaseClass
{
    public HPComponent hp;
    public MoveComponent mover;
    //public ShootingComponent shooter;

    public GameObject otherBro;
    public GameObject lance;

    public float[] stateTimes = new float[3];

    public float stateTimer = 0f;

    public enum States
    {
        IDLE, //0.5
        MOVE, //2
        DEATH //3
    }
    public States state = States.IDLE;

    float lanceAngle = 0f;
    
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= atkDist)
            {
                isAlert = true;
            }
            if (isAlert)
            {
                stateTimer += Time.deltaTime;
                switch(state)
                {
                    case States.IDLE:
                        CheckHealth();
                        lance.transform.localRotation = Quaternion.Euler(0f, GetMoveAng() + 180f, 0f);//eulerAngles = new Vector3(0f, GetMoveAng() + 180f, 0f);
                        if (stateTimer > stateTimes[0])
                        {
                            stateTimer = 0f;
                            lanceAngle = GetMoveAng();
                            state = States.MOVE;
                        }
                        break;
                    case States.MOVE:
                        CheckHealth();
                        if (stateTimer > stateTimes[1])
                        {
                            stateTimer = 0f;
                            state = States.IDLE;
                        }
                        transform.eulerAngles = new Vector3(0f, lanceAngle, 0f);
                        mover.MoveAngularly(transform.forward * GameManager.gm.timeSlowMulti);
                        mover.ResetY();
                        transform.eulerAngles = Vector3.zero;
                        break;
                    case States.DEATH:
                        if (stateTimer > stateTimes[2] && otherBro.GetComponent<BossLancerBrother>().state == States.DEATH)
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