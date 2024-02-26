using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseScreen;
    
    private bool isPaused = false;

    void Awake()
    {
        pauseScreen.SetActive(false);
    }
    
    public void TogglePause()
    {
        if (!isPaused)
            Pause();
        else
            Resume();
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("Resuming game");
        isPaused = !isPaused;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
}
