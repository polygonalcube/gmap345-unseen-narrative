using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public SpawningComponent spawner;
    public GameObject[] bulletPool = new GameObject[1000];
    
    void Awake() //Allows for Singleton.
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CreateBulletPool();
    }

    void Update()
    {
        
    }

    public int Sign(float num)
    {
        if (num < 0)
        {
            return -1;
        }
        else if (num > 0)
        {
            return 1;
        }
        return 0;
    }

    void CreateBulletPool()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            GameObject newProjectile = spawner.Spawn(Vector3.zero);
            newProjectile.SetActive(false);
            bulletPool[i] = newProjectile;
        }
    }

    public GameObject RetrieveBullet(Vector3 activationPosition)
    {
        int i = 0;
        while (i < bulletPool.Length)
        {
            if (!bulletPool[i].activeSelf)
            {
                bulletPool[i].SetActive(true);
                bulletPool[i].transform.position = activationPosition;
                return bulletPool[i];
                break;
            }
            i++;
        }
        return bulletPool[0];
    }
}
