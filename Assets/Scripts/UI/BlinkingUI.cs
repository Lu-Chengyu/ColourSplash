using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    public float blinkInterval = 0.3f; // Interval in seconds for the blink
    private Image uiElement; // Reference to the UI Image component

    void Start()
    {
        uiElement = GetComponent<Image>(); // Get the Image component
        Debug.Log(uiElement);
    }

    public void startBlink()
    {
        uiElement = GetComponent<Image>();
        if(FindObjectOfType<LevelUIController>().isVG)
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            uiElement.enabled = !uiElement.enabled; // Toggle the visibility
            yield return new WaitForSeconds(blinkInterval); // Wait for some time
        }
    }
}
