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
        if (isVictory)
        {
            Time.timeScale = 0f;
            gameVictoryScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
    }
}