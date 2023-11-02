using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Image healthMask;
    public Sprite healthy;
    public Sprite hurt;
    public GameObject deathMenu; 
    public GameObject pauseMenu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<HPComponent>().health < 2)
        {
            healthMask.sprite = hurt;
        }
        else
        {
            healthMask.sprite = healthy;
        }

        if (player.GetComponent <HPComponent>().health == 0) 
        {
            deathMenu.SetActive(true);
        }
    }
}
