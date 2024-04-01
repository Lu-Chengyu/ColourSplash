using System;
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

    }
    public void LoadLevel(string levelName)
    {
        FindObjectOfType<IDManager>().deleteSessionID();
        FindObjectOfType<IDManager>().createSessionID();
        //PlayerPrefs.SetInt("currentLevel", Convert.ToInt32(levelName));
        SceneManager.LoadScene(levelName);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
