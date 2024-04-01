using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Finish line triggered!");
        if (other.CompareTag("Player"))
        {
            // FindObjectOfType<AnalyticRecorder>().postToDB();
            // FindAnyObjectByType<AnalyticRecorder>().recordAttempt(gameManager.currentLevel, PlayerPrefs.GetInt("attempts", 1));
            
            Debug.Log("Player reached the finish line!");
            gameManager.LevelClear();
        }
    }
}
