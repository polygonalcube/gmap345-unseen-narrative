using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    //base class for all cooldown UI
    public float sliderValue;
    public float timeMulti; //change this value to change how fast the bar fills
    public Slider slider;
    protected PlayerLogic playerScript;
    protected bool actionCheck;

    public virtual void Start()
    {
        playerScript = GameManager.gm.FindPlayerScript();
    }

    public virtual void Update()
    {
        if (playerScript == null)
        {
            playerScript = GameManager.gm.FindPlayerScript();
        }
        else if (actionCheck == true)
        {
            slider.value = 0f;
            sliderValue = 0f;
        }
        else
        {
            BarFill();
        }
    }

    public void BarFill()
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, sliderValue);
        sliderValue += Time.deltaTime * timeMulti;
    }
}
