using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    public float addAngle = 90f;
    protected GameObject player;

    public float atkDist = 35f;
    protected bool isAlert = false;

    public virtual void Start()
    {
        isAlert = false;
        player = GameManager.gm.FindPlayer();
    }

    public virtual void Update()
    {
        if (player == null)
        {
            player = GameManager.gm.FindPlayer();
        }
    }

    protected float AngleToObject(Vector2 from, Vector2 to)
    {
        //posOrNeg * (Mathf.Atan2(to.y - from.y , to.x - from.x) * 180 / Mathf.PI) + addAngle;
        
        float noDivByZero = 0f;
        if ((to.x - from.x) == 0)
        {
            noDivByZero = 0.00001f;
        }
        float slope = Mathf.Atan((to.y - from.y) / (to.x - from.x + noDivByZero)) * Mathf.Rad2Deg * -1f;
        if (to.x < from.x)
        {
            slope = Mathf.Abs(slope);
            slope = 180f - slope;
            if (to.y > from.y)
            {
                slope = 360f - slope;
            }
        }
        else if (to.y > from.y)
        {
            slope = Mathf.Abs(slope);
            slope = 180f - slope;
            slope += 180f;
        }

        return slope + addAngle;
    }
}