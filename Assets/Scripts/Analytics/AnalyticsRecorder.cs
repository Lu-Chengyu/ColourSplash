using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class AnalyticsRecorder : MonoBehaviour
{
    public List<ColorChangeRecord> colorChangeRecords;
    public DeathRecord deathRecord;
    public colorUsage colorUsage;
    public abilityUsageCollection abilityUsage;
    private float startTime;


    private void Start()
    {
        Debug.Log("AnalyticsRecorder started");
        colorChangeRecords = new List<ColorChangeRecord>();
        startTime = Time.time;
        colorUsage = new colorUsage(startTime);
        abilityUsage = new abilityUsageCollection();
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
        deathRecord = new DeathRecord(position, Time.time - startTime);
        // string json = JsonUtility.ToJson(deathRecord);
        string json = JsonUtility.ToJson(colorUsage);
        Debug.Log(json);
    }

    public void postToDB()
    {
        int UID = FindObjectOfType<IDManager>().getUserID();
        int sessionID = FindObjectOfType<IDManager>().getSessionID();
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        UploadData data = new UploadData(colorChangeRecords, colorUsage, abilityUsage, deathRecord, currentLevel);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        string url = string.Format("https://csci526-62116-default-rtdb.firebaseio.com/{0}/{1}.json", UID, sessionID);
        RestClient.Post(url, json).Then(response => {
            return;
        });
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
public class colorUsage
{
    enum PlayerColor { Red, Green, Blue }
    public float redUsage;
    public float blueUsage;
    public float greenUsage;
    private float timeLastSwitched;

    public colorUsage(float time)
    {
        this.redUsage = 0f;
        this.blueUsage = 0f;
        this.greenUsage = 0f;
        this.timeLastSwitched = time;
    }

    public void SwitchColor(string currentColor)
    {
        float currentTime = Time.time;
        float timeInCurrentColor = currentTime - timeLastSwitched;
        timeLastSwitched = currentTime;
        switch (currentColor)
        {
            case "Red":
                redUsage += timeInCurrentColor;
                break;
            case "Green":
                greenUsage += timeInCurrentColor;
                break;
            case "Blue":
                blueUsage += timeInCurrentColor;
                break;
        }
    }
}

[Serializable]
public class abilityUsageCollection
{
    public List<abilityUsage> redAbilityUsage;
    public List<abilityUsage> blueAbilityUsage;
    public List<abilityUsage> greenAbilityUsage;

    public abilityUsageCollection()
    {
        this.redAbilityUsage = new List<abilityUsage>();
        this.blueAbilityUsage = new List<abilityUsage>();
        this.greenAbilityUsage = new List<abilityUsage>();
    }

    public void recordAbilityUsage(Vector3 position, string ability, float time)
    {
        switch (ability)
        {
            case "Red":
                redAbilityUsage.Add(new abilityUsage(position, time));
                break;
            case "Green":
                greenAbilityUsage.Add(new abilityUsage(position, time));
                break;
            case "Blue":
                blueAbilityUsage.Add(new abilityUsage(position, time));
                break;
        }
    }
}

[Serializable]
public class abilityUsage
{
    public Vector3 position;
    public float time;

    public abilityUsage(Vector3 position, float time)
    {
        this.position = position;
        this.time = time;
    
    }
}

public class UploadData
{
    public int level;
    public List<ColorChangeRecord> colorChangeRecords;
    public colorUsage colorUsage;
    public abilityUsageCollection abilityUsage;
    public DeathRecord deathRecord;
    
    public UploadData(List<ColorChangeRecord> colorChangeRecords, colorUsage colorUsage, abilityUsageCollection abilityUsage, DeathRecord deathRecord, int level)
    {
        this.colorChangeRecords = colorChangeRecords;
        this.colorUsage = colorUsage;
        this.abilityUsage = abilityUsage;
        this.deathRecord = deathRecord;
        this.level = level;
    }
}