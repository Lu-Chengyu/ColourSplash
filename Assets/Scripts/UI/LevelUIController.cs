using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    public GameObject instructionScreen;
    public GameObject pauseScreen;
    public Transform player;
    
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
        FindObjectOfType<IDManager>().deleteSessionID();
        FindObjectOfType<IDManager>().createSessionID();
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1) + 1;
        Debug.Log("Next level: " + currentLevel);
        SceneManager.LoadScene(currentLevel.ToString());
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
        Debug.Log("Game restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //player.position = new Vector3(-16, -3, 0);
        //instructionScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void ResumeGame()
    {
        FindObjectOfType<PauseGame>().Resume();
    }
}
