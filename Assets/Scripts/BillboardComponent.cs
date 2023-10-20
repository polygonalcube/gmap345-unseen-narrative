using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardComponent : MonoBehaviour
{
    public float addTilt = 0f;
    GameObject cam;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    
    void Update()
    {
        transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + addTilt, 0f, 0f);
        //transform.LookAt(cam.transform.position, -Vector3.forward);
    }
}
