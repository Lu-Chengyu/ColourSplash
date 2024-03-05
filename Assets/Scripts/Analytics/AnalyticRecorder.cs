using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Proyecto26;
using UnityEditor;

public class AnalyticRecorder : MonoBehaviour
{
    public List<ColorChangeRecord> colorChangeRecords;
    public List<DeathRecord> deathRecords;

    private void Start()
    {
        colorChangeRecords = new List<ColorChangeRecord>();
        deathRecords = new List<DeathRecord>();
    }

    public void recordColorChange(string currentColor, string nextColor, Vector3 position)
    {
        ColorChangeRecord record = new ColorChangeRecord(currentColor, nextColor, Time.time, position);
        colorChangeRecords.Add(record);
        string json = JsonUtility.ToJson(record);
        Debug.Log(json);
    }

    public void recordDeath(Vector3 position)
    {
        DeathRecord record = new DeathRecord(position, Time.time);
        deathRecords.Add(record);
        string json = JsonUtility.ToJson(record);
        Debug.Log(json);
        postToDB();
    }

    public void postToDB()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        UploadData data = new UploadData(colorChangeRecords, deathRecords, currentLevel);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        RestClient.Post("https://csci526-62116-default-rtdb.firebaseio.com/.json", json).Then(response => {
            return;
        }); ;


    }
}

[Serializable]
public class DeathRecord
{
    public Vector3 position;
    public float time;

    public DeathRecord(Vector3 position, float time)
    {
        this.position = position;
        this.time = time;
    }
}

[Serializable]
public class ColorChangeRecord
{
    public string currentColor;
    public string nextColor;
    public float time;
    public Vector3 position;

    public ColorChangeRecord(string currentColor, string previousColor, float time, Vector3 position)
    {
        this.currentColor = currentColor;
        this.nextColor = previousColor;
        this.time = time;
        this.position = position;
    }
}

[Serializable]
public class UploadData
{
    public List<ColorChangeRecord> colorChangeRecords;
    public List<DeathRecord> deathRecords;
    public int level;

    public UploadData(List<ColorChangeRecord> colorChangeRecords, List<DeathRecord> deathRecords, int level)
    {
        this.colorChangeRecords = colorChangeRecords;
        this.deathRecords = deathRecords;
        this.level = level; 
    }
}
