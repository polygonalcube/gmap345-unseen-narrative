using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyBaseClass
{
    public SpawningComponent spawner;

    float spawnTimer = 0f;
    public float spawnTimerSet = 5f;

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, player.transform.position) <= atkDist)
        {
            isAlert = true;
        }
        if (isAlert)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawner.Spawn(transform.position);
                spawnTimer = spawnTimerSet;
            }
        }
    }
}
