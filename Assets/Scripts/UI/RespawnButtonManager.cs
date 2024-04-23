using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnButtonManager : MonoBehaviour
{
    public Button respawnButton;
    // Start is called before the first frame update
    void Start()
    {
        // respawnButton = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        respawnButton.interactable = PlayerPrefs.GetInt("reachCheckpoint") > 0;
    }
}
