using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardComponent : MonoBehaviour
{
    GameObject cam;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    
    void Update()
    {
        transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, 0, 0);
    }
}
