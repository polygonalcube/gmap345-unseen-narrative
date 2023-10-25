using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    public HPComponent hp;
    public MoveComponent mover;
    
    void Start()
    {
        
    }

    void Update()
    {
        mover.MoveAngularly(transform.forward);
        if (hp != null)
        {
            if (hp.health <= 0)
            {
                hp.health = 1;
                this.gameObject.SetActive(false);
            }
        }
    }
}
