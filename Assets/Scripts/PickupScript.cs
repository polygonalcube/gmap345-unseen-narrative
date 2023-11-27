using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    public GameObject popup;
    public string itemName;
    public string itemDescription;
    public Image itemSprite;
    public GameObject UI;
    public GameObject player;
    public string totemRefName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            UI.GetComponent<PopupUIScript>().currentDescription = itemDescription;
            UI.GetComponent<PopupUIScript>().currentName = itemName;
            UI.GetComponent<PopupUIScript>().currentSprite = itemSprite;
            UI.SetActive(true);
            player.GetComponent<PlayerLogic>().totemsHeld.Add(totemRefName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popup.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        popup.SetActive(false);
    }
}
