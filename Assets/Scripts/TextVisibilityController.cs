using UnityEngine;
using UnityEngine.UI;

public class TextVisibilityController : MonoBehaviour
{
    public Transform targetObject; // The object whose position we're tracking
    public Text textToShow; // The text to show or hide
    public Text endGamePrompt; // The end game prompt text
    public float thresholdPositionX = 0f; // The threshold position

    private bool isTextVisible = true; // Initial visibility state of the text
    private bool isGameOver = false; // Flag to track game over state

    void Start()
    {
        endGamePrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (targetObject.position.x > 25)
        {
            isGameOver = true;
        }

        // Check if the game is over
        if (!isGameOver)
        {
            // Check if the target object's X position exceeds the threshold
            if (targetObject.position.x > thresholdPositionX)
            {
                // If the text is currently visible, hide it
                if (isTextVisible)
                {
                    textToShow.gameObject.SetActive(false);
                    isTextVisible = false;
                }
            }
            else
            {
                // If the text is currently hidden, show it
                if (!isTextVisible)
                {
                    textToShow.gameObject.SetActive(true);
                    isTextVisible = true;
                }
            }
        }
        else
        {
            // If the game is over, display the end game prompt
            endGamePrompt.gameObject.SetActive(true);
        }
    }
}
