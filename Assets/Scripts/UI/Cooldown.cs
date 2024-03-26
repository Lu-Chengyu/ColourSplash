using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System; // Required when Using UI elements.

public class Cooldown : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            cooldown.fillAmount += Time.deltaTime / waitTime;
        }
    }
    public void StartCooldown()
    {
        cooldown.fillAmount = 0;
        coolingDown = true;
    }

    public void StopCooldown()
    {
        coolingDown = false;
    }
}
