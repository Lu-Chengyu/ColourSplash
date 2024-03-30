using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button red;
    public Button green;
    public Button blue;

    private PlayerColorChange playerColorChange;

    // Start is called before the first frame update
    void Start()
    {
        red.interactable = false;
        green.interactable = false;
        blue.interactable = false;

        playerColorChange = FindObjectOfType<PlayerColorChange>();
        if (playerColorChange == null)
        {
            Debug.LogError("PlayerColorChange component not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerColorChange == null) return;

        Color currentColor = playerColorChange.currentColor;

        red.interactable = currentColor.r != 0;
        green.interactable = currentColor.g != 0;
        blue.interactable = currentColor.b != 0;
    }
}
