using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float sliderValue;
    public Slider slider;
    public GameObject player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerLogic>().totemActive == true)
        {
            sliderValue = Time.deltaTime;
            slider.value = sliderValue;
        }
        else
        {
            slider.enabled = false;
        }
        
    }
}
