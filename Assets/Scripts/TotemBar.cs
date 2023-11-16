using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float sliderValue;
    public float minValue = 0f;
    public float maxValue = 6f;
    public float timeEdit; /* Change this value to change how fast the bar fills*/
    public Slider slider;
    public GameObject player;


    void Start()
    {
        slider.maxValue = maxValue;
        slider.minValue = minValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerLogic>().totemActive == true)
        {
            slider.value = 0f;
            sliderValue = 0f;
        }
        else
        {
            BarFill();
        }
        
    }

    void BarFill()
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, sliderValue);
        sliderValue += Time.deltaTime * timeEdit;
    }
}
