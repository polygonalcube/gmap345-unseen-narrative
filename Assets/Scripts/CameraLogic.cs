using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    GameObject player;
    
    
    //float zoom = 0f;
    //public float zoomMultiplier = 1f;
    Vector3 target = Vector3.zero;
    Vector3 refVelocity = Vector3.zero;
    public float smoothTime = .5f;
    public float maxSpeed = Mathf.Infinity;

    public Vector3 offset = Vector3.zero;
    public float speedMultiplier = 0.5f;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.TryGetComponent<MoveComponent>(out MoveComponent mover))
        {
            target = new Vector3(player.transform.position.x + mover.xSpeed * speedMultiplier, transform.position.y, 
            player.transform.position.z + mover.zSpeed * speedMultiplier);

            transform.position = Vector3.SmoothDamp(transform.position, target + offset, ref refVelocity, smoothTime, maxSpeed);
        }
    }
}
