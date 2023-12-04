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
        player = GameManager.gm.FindPlayer();
    }

    void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerLogic>().camZone != null)
            {
                Vector3 camZone = player.GetComponent<PlayerLogic>().camZone.transform.position;
                target = new Vector3(camZone.x + offset.x, offset.y, camZone.z + offset.z);
                if (player.GetComponent<PlayerLogic>().camZone.transform.localScale.x > 1.5f)
                {
                    target = new Vector3(camZone.x + offset.x, offset.y * (player.GetComponent<PlayerLogic>().camZone.transform.localScale.x / 1.333f), camZone.z + offset.z);
                }
            }
            else if (player.TryGetComponent<MoveComponent>(out MoveComponent mover))
            {
                target = new Vector3(player.transform.position.x + mover.xSpeed * speedMultiplier, transform.position.y, 
                player.transform.position.z + mover.zSpeed * speedMultiplier);
            }
            transform.position = Vector3.SmoothDamp(transform.position, target, ref refVelocity, smoothTime, maxSpeed);
        }
        else
        {
            player = GameManager.gm.FindPlayer();
        }
    }
}
