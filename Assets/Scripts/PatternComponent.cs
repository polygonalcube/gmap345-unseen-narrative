using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternComponent : MonoBehaviour
{
    public int bulletCount = 1;

    public bool isSpread = false;
    public float spreadAngle = 45f;

    public bool isRadial = false;

    public bool isAlternating = false;

    public float[] Spread()
    {
        float[] angles = new float[bulletCount];
        for (int i = 0; i < bulletCount; i++)
        {
            /*
            vulcan.shotAngle = pointer.x - 270f + ((float)(i-2) * 15f);
            if (player.position.x > vulcan.shotOrigin.position.x)
            {
                vulcan.shotAngle = -vulcan.shotAngle;
            }
            vulcan.ShootAng(vulcan.bulletSpeedAng);
            vulcan.shotTimer = 0f;
            */
            angles[i] = 0f;
        }
        return angles;
    }
}
