using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtComponent : MonoBehaviour
{
    public HPComponent hp;
    //public ScoreComponent score;
    public LayerMask layers;
    public string[] tags;

    public int damageOverride;
    public bool immediateDeath = true;

    public bool hasIFrames = false;
    public float iSeconds = 0f;
    public float iSecondsSet;
    public GameObject meshRen;
    public GameObject sprRen;

    public bool active = true;

    void Update()
    {
        if (hasIFrames)
        {
            iSeconds -= Time.deltaTime;
            if (iSeconds > 0f)
            {
                if (meshRen.activeSelf)
                {
                    meshRen.SetActive(false);
                }
                else
                {
                    meshRen.SetActive(true);
                }
            }
            else
            {
                meshRen.SetActive(true);
            }
        }
        else
        {
            if (meshRen != null)
            {
                if (!meshRen.activeSelf)
                {
                    StartCoroutine(BecomeVisible());
                }
            }
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(1f/30f);
        meshRen.SetActive(true);
    }

    bool CheckTags(Collider col)
    {
        foreach (string tag in tags)
        {
            if (col.gameObject.tag == tag)
            {
                return true;
            }
        }
        return false;
    }
    
    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter(Collider col)
    {
        if (CheckTags(col) && active)//((layers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
        {
            if (hp != null)
            {
                if (hasIFrames && iSeconds > 0f)
                {
                    //Do nothing
                }
                else if (damageOverride != 0)
                {
                    hp.health -= damageOverride;
                    if (hasIFrames)
                    {
                        iSeconds = iSecondsSet;
                    }
                }
                else if (col.gameObject.TryGetComponent<HitComponent>(out HitComponent hitbox))
                {
                    hp.health -= hitbox.damage;
                    if (hasIFrames)
                    {
                        iSeconds = iSecondsSet;
                    }
                }
                /*
                if (score != null)
                {
                    GameManager.gm.score += score.damagePoints;
                }
                */
                /*
                if (col.gameObject.tag != "Player")
                {
                    Destroy(col.gameObject);
                }
                */
                if (hp.health <= 0)
                {
                    /*
                    if (score != null)
                    {
                        GameManager.gm.score += score.deathPoints;
                    }
                    */
                    if (immediateDeath)
                        Destroy(this.gameObject);
                }

                if (meshRen != null && !hasIFrames)
                {
                    meshRen.SetActive(false);
                }
            }
        }
    }
}
