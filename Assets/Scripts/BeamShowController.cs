using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShowController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject beamLight;
    public int showTime;
    public int hideTime;
    private bool show;
    void Start()
    {
        beamLight = GameObject.Find("Beam");
        show = true;
    }

    // Update is called once per frame
    void Update()
    {
        toggleBeam();
    }

    public void toggleBeam()
    {
        if (show)
        {
            StartCoroutine(showBeamRoutine(showTime));
        }
        else
        {
            StartCoroutine(hideBeamRoutine(hideTime));
        }
        
    }

    IEnumerator showBeamRoutine(int seconds)
    {
        beamLight.SetActive(true);
        int i = 0;
        while (i < seconds)
        {
            i += 1;
            yield return new WaitForSeconds(1.0f);
        }

        show = false;
    }

    IEnumerator hideBeamRoutine(int seconds)
    {
        beamLight.SetActive(false);
        int i = 0;
        while (i < seconds)
        {
            i += 1;
            yield return new WaitForSeconds(1.0f);
        }

        show = true;
    }
}
