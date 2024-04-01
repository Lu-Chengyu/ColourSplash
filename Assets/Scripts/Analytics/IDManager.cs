using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void createUserID()
    {
        int UID = getUserID();
        PlayerPrefs.SetInt("UID", UID);
        PlayerPrefs.Save();
    }

    public int getUserID()
    {
        return PlayerPrefs.GetInt("UID", UnityEngine.Random.Range(100000000, 999999999));
    }

    public void createSessionID()
    {
        int id = Random.Range(100000000, 999999999);
        PlayerPrefs.SetInt("sessionID", id);
    }

    public int getSessionID()
    {
        return PlayerPrefs.GetInt("sessionID", 0);
    }

    public void deleteSessionID()
    {
        PlayerPrefs.DeleteKey("sessionID");
    }
}
