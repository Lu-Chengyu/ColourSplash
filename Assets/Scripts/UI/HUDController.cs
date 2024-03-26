using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button red;
    public Button green;
    public Button blue;
    // Start is called before the first frame update
    void Start()
    {
        red.interactable = false;
        green.interactable = false;
        blue.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Color currentColor = FindObjectOfType<PlayerColorChange>().currentColor;
        if(currentColor.r != 0)
        {
            red.interactable = true;
            green.interactable = false;
            blue.interactable = false;
        }
        if (currentColor.g != 0)
        {
            green.interactable = true;
            red.interactable = false;
            blue.interactable = false;
        }
        if (currentColor.b != 0)
        {
            blue.interactable = true;
            red.interactable = false;
            green.interactable = false;
        }
    }
}
