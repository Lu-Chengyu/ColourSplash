using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ResumeFromCheckpoint()
    {
        Debug.Log("Resume from checkpoint");
        PlayerPrefs.SetInt("fromCheckpoint", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}