using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardComponent : MonoBehaviour
{
    //Makes objects point towards the camera
    public float addTilt = 0f;
    GameObject cam;
    
    void Start()
    {
        FindCamera();
    }
    
    void Update()
    {
        if (cam == null)
        {
            FindCamera();
        }
        else
        {
            transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + addTilt, 0f, 0f);
        }
    }

    void FindCamera()
    {
        cam = GameObject.Find("Main Camera");
    }
}
