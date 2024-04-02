using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameVictoryScreen;

    void Awake()
    {
        gameOverScreen.SetActive(false);
        gameVictoryScreen.SetActive(false);
    }

    public void End(bool isVictory)
    {
        FindObjectOfType<AnalyticsRecorder>().postToDB();
        if (isVictory)
        {
            Time.timeScale = 0f;
            gameVictoryScreen.SetActive(true);
            FindObjectOfType<IDManager>().deleteSessionID();
        }
        else
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
    }
}