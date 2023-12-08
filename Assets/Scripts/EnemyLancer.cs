using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLancer : EnemyBaseClass
{
    public MoveComponent mover;

    public Vector2 idealDist = new Vector2(3f, 5f);
    public float[] stateTimes = new float[2];
    public float stateTimer = 0f;

    public GameObject lance;
    float lanceAngle = 0f;

    public enum States
    {
        IDLE,
        MOVE
    }
    public States state = States.MOVE;

    public override void Update()
    {
        base.Update();
        if (player != null)
        {
            stateTimer += Time.deltaTime;
            switch(state)
            {
                case States.IDLE:
                    stateTimer += Time.deltaTime;
                    lance.transform.localRotation = Quaternion.Euler(0f, GetMoveAng() + 180f, 0f);
                    if (stateTimer > stateTimes[0])
                    {
                        stateTimer = 0f;
                        lanceAngle = GetMoveAng();
                        state = States.MOVE;
                    }
                    break;
                case States.MOVE:
                    if (stateTimer > stateTimes[1])
                    {
                        stateTimer = 0f;
                        state = States.IDLE;
                    }
                    transform.eulerAngles = new Vector3(0f, lanceAngle, 0f);
                    mover.MoveAngularly(transform.forward * GameManager.gm.timeSlowMulti); //
                    mover.ResetY();
                    transform.eulerAngles = Vector3.zero;
                    break;
            }
        }
    }  

    float GetMoveAng()
    {
        return AngleToObject(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));
    } 
}
