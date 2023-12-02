using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBaseClass
{
    public ShootingComponent shooter;
    
    public override void Update()
    {
        base.Update();
        shooter.Shoot(transform.position, positionOffset: Vector3.zero, 
        shotDirection: new Vector3(0f, AngleToObject(new Vector2(transform.position.x, transform.position.z), 
        new Vector2(player.transform.position.x, player.transform.position.z)), 0f), targetPosition: player.transform.position);
    }
}
