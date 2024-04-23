using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameVictoryScreen;
    public GameObject gameEndScreen;
    public PlayerColorChange playerColorChange;

    void Awake()
    {
        gameOverScreen.SetActive(false);
        gameVictoryScreen.SetActive(false);
        playerColorChange = FindAnyObjectByType<PlayerColorChange>();
    }

    public void End(bool isVictory)
    {
        FindObjectOfType<AnalyticsRecorder>().postToDB();
        if (isVictory)
        {
            if(FindObjectOfType<GameManager>().currentLevel == 12)
            {
                Time.timeScale = 0f;
                gameEndScreen.SetActive(true);
                FindObjectOfType<IDManager>().deleteSessionID();
            }
            else
            {
                Time.timeScale = 0f;
                gameVictoryScreen.SetActive(true);
                FindObjectOfType<IDManager>().deleteSessionID();
            }
            
        }
        else
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
    }

    public void ResumeFromCheckpoint()
    {
        Debug.Log("Resume from checkpoint");
        playerColorChange.gameObject.SetActive(true);
        PlayerPrefs.SetInt("fromCheckpoint", 1);
        PlayerPrefs.Save();
        gameOverScreen.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}