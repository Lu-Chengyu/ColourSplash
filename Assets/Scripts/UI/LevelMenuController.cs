using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    
    public Button[] levelButtons;
    
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("maxLevel", 1);;

        // Assuming levelButtons are ordered by level index
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Unlock all levels up to and including the current level
            levelButtons[i].interactable = i < currentLevel;
        }
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
