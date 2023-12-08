using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningComponent : MonoBehaviour
{
    public GameObject spawnedObject;
    
    public GameObject Spawn(Vector3 spawnPosition, GameObject parent = null, bool localPositioning = false)
    {
        GameObject newObject = Instantiate(spawnedObject, spawnPosition, Quaternion.identity);
        if (parent != null)
        {
            newObject.transform.SetParent(parent.transform);
        }
        return newObject;
    }
}
