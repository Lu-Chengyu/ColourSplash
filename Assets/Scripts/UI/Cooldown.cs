using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System; // Required when Using UI elements.
using Unity.VisualScripting;

public class Cooldown : MonoBehaviour
{
    public Image cooldown;
    public Button cooldownButton;
    public bool coolingDown;
    public float waitTime = 2.0f;

    private void Start()
    {
        Debug.Log(cooldown.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            cooldown.fillAmount += Time.deltaTime / waitTime;
            if(cooldown.fillAmount >= 1.0f && coolingDown)
            {
                Debug.Log("cd ended");
                StopCooldown();
            }
        }
    }

    public void StartCooldown()
    {
        cooldown.fillAmount = 0;
        cooldown.color = Color.grey;
        coolingDown = true;
    }

    public void StopCooldown()
    {
        cooldown.color = Color.white;
        coolingDown = false;
    }
}
