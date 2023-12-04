using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public GameObject player;
    public Image healthMask;
    public Sprite healthy;
    public Sprite hurt;
    public GameObject deathMenu; 
    public GameObject pauseMenu;

    void Update()
    {
        if (player.GetComponent<HPComponent>().health < 2)
            healthMask.sprite = hurt;
        else
            healthMask.sprite = healthy;

        if (player.GetComponent <HPComponent>().health == 0) 
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SwapScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
