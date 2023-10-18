using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    public MoveComponent mover;
    
    void Start()
    {
        
    }

    void Update()
    {
        mover.MoveAngularly(transform.forward);
    }
}
