using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShowController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject beamLight;
    public float showTime;
    public float hideTime;
    private bool show;
    void Start()
    {
        beamLight = transform.Find("Beam").gameObject;
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

    IEnumerator showBeamRoutine(float seconds)
    {
        beamLight.SetActive(true);
        float i = 0;
        while (i < seconds)
        {
            i += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        show = false;
    }

    IEnumerator hideBeamRoutine(float seconds)
    {
        beamLight.SetActive(false);
        float i = 0;
        while (i < seconds)
        {
            i += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        show = true;
    }
}
