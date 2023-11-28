using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUIScript : MonoBehaviour
{
    public TextMeshProUGUI uiName;
    public TextMeshProUGUI uiDescription;
    public Image uiImage;

    public string currentName;
    public string currentDescription;
    public Image currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        uiDescription.text = currentDescription;
        uiName.text = currentName;
        uiImage = currentSprite;
        StartCoroutine(HideUI());
    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
