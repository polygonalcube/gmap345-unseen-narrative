using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public SpawningComponent spawner;
    public GameObject[] bulletPool = new GameObject[10000];

    public float timeSlowMulti = 1f;
    public float timeSlowMultiSet = 0.25f;
    
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

    public GameObject FindPlayer()
    {
        return GameObject.Find("Player");
    }

    public PlayerLogic FindPlayerScript()
    {
        return GameObject.Find("Player").GetComponent<PlayerLogic>();
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

    public GameObject RetrieveBullet(Vector3 activationPosition, string newTag = "Enemy Damage")
    {
        int i = 0;
        while (i < bulletPool.Length)
        {
            if (!bulletPool[i].activeSelf)
            {
                bulletPool[i].transform.position = activationPosition;
                bulletPool[i].tag = newTag;
                bulletPool[i].SetActive(true);
                return bulletPool[i];
                //break;
            }
            i++;
        }
        return bulletPool[0];
    }
}
