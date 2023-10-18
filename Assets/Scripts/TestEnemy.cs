using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public ShootingComponent[] shooters;

    GameObject player;

    public float posOrNeg = -1f;
    public float addAngle = 90f;

    float stillAngle;
    
    void Start()
    {
        player = GameObject.Find("Player");
        stillAngle = AngleToObject(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            shooters[i].Shoot(transform.position, 
            positionOffset: Vector3.zero, 
            shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
            new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
        }
        //Debug.Log(AngleToObject(new Vector2(transform.position.x, transform.position.x), 
        //new Vector2(player.transform.position.x, player.transform.position.z)));
    }

    float AngleToObject(Vector2 from, Vector2 to)
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
    /*
    if (point_b.position.x - point_a.position.x) == 0:
		insurance = 0.000001
	else:
		insurance = 0
	slope = -rad_to_deg(atan((point_b.position.y - point_a.position.y)/(point_b.position.x - point_a.position.x + insurance)))
	if point_b.position.x < point_a.position.x:
		slope = abs(slope)
		slope = 180 - slope
		if point_b.position.y > point_a.position.y:
			slope = 360 - slope
	elif point_b.position.y > point_a.position.y:
		slope = abs(slope)
		slope = 180 - slope
		slope += 180
    */
}
