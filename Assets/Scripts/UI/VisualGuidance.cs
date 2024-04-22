using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGuidance : MonoBehaviour
{
    public GameObject[] tutorialSteps;
    public GameObject closeButton;
    private int currentStep = 0;
    private bool completed = false;

    void Start()
    {
        closeButton.SetActive(false);
        ShowStep(currentStep);
    }

    void Update()
    {
        if (currentStep == tutorialSteps.Length - 1)
        {
            closeButton.SetActive(true);
        }
        //if (Input.GetKeyDown(KeyCode.N)) // Assuming 'N' moves to the next step
        //{
        //    NextStep();
        //}
    }

    void ShowStep(int step)
    {
        HideAllSteps();
        tutorialSteps[step].SetActive(true);
        tutorialSteps[step].GetComponentInChildren<BlinkingUI>().startBlink();
    }

    public void PreviousStep()
    {
        Debug.Log("previous");
        if (currentStep > 0)
        {
            currentStep--;
            ShowStep(currentStep);
        }
    }

    public void NextStep()
    {
        Debug.Log("next");
        if (currentStep < tutorialSteps.Length - 1)
        {
            currentStep++;
            ShowStep(currentStep);
        }
    }

    public void HideAllSteps()
    {
        
        foreach (GameObject step in tutorialSteps)
        {
            step.SetActive(false);
        }
    }
}
