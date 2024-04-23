using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // public Text redCounterText;
    // public Text greenCounterText;
    // public Text blueCounterText;
    public int currentLevel;
    public int maxLevel;
    public Transform player;
    public GameObject ckptText;

    // private int redCount;
    // private int greenCount;
    // private int blueCount;

    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("fromCheckpoint", 0);
        PlayerPrefs.SetString("lastColor", "Red");
        PlayerPrefs.SetInt("reachCheckpoint", 0);
        PlayerPrefs.Save();
    }

    void Start()
    {
        // redCount = 1;
        // greenCount = 1;
        // blueCount = 1;

        PlayerPrefs.SetInt("currentLevel", currentLevel);

        if(currentLevel == 1)
        {
            PlayerPrefs.SetInt("level1Firsttime", 0);
        }

        if (PlayerPrefs.GetInt("fromCheckpoint") == 0)
        {
            // checkpointPosition = transform.position;
            PlayerPrefs.SetFloat("ckpt_x", PlayerPrefs.GetFloat("init_x"));
            PlayerPrefs.SetFloat("ckpt_y", PlayerPrefs.GetFloat("init_y"));
        }
        PlayerPrefs.Save();

        Time.timeScale = 1f;

        // UpdateCounters();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<PauseGame>().TogglePause();
        }
    }

    // public bool IsColorAvailable(Color color)
    // {
    //     if (color == Color.red)
    //         return redCount > 0;
    //     else if (color == Color.green)
    //         return greenCount > 0;
    //     else if (color == Color.blue)
    //         return blueCount > 0;
    //     return false;
    // }

    // public void UpdateCounter(Color color)
    // {
    //     if (color == Color.red)
    //         redCount++;
    //     else if (color == Color.green)
    //         greenCount++;
    //     else if (color == Color.blue)
    //         blueCount++;
    //     UpdateCounters();
    // }

    // public void UpdateCounter(string color, int incrementValue)
    // {
    //     switch (color)
    //     {
    //         case "Red":
    //             redCount += incrementValue;
    //             break;
    //         case "Green":
    //             greenCount += incrementValue;
    //             break;
    //         case "Blue":
    //             blueCount += incrementValue;
    //             break;
    //     }
    //     UpdateCounters();
    // }

    // void UpdateCounters()
    // {
    //     redCounterText.text = "" + redCount;
    //     greenCounterText.text = "" + greenCount;
    //     blueCounterText.text = "" + blueCount;
    // }

    public void DestroyGameInstance()
    {
        // Application.Quit();
       
        FindObjectOfType<AnalyticsRecorder>().recordDeath(player.position);
        PlayerPrefs.SetInt("level1Firsttime", 1);
        FindObjectOfType<EndGame>().End(false);
    }

    //Call this method when player reaches the end of the level
    public void LevelClear()
    {
        // Application.Quit();
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log("Current level: " + currentLevel);
        if (currentLevel == maxLevel)
        {
            Debug.Log("Reached maxlevel");
            return;
        }
        PlayerPrefs.SetInt("maxLevel", currentLevel + 1);
        PlayerPrefs.Save();
        FindObjectOfType<EndGame>().End(true);
    }

    public void UpdateCheckpointPosition(Vector2 position)
    {
        PlayerPrefs.SetFloat("ckpt_x", position.x);
        PlayerPrefs.SetFloat("ckpt_y", position.y);
        PlayerPrefs.Save();
    }
}
