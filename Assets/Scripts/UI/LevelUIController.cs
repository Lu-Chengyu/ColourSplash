using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    public GameObject instructionScreen;
    
    void Awake()
    {
        instructionScreen.SetActive(false);
    }
    
    public void ToggleInstructionScreen()
    {
        instructionScreen.SetActive(!instructionScreen.activeSelf);
    }
    
    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        SceneManager.LoadScene("1-" + (currentLevel + 1));
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    
    public void ResumeGame()
    {
        FindObjectOfType<PauseGame>().Resume();
    }
}
