using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInstruction : MonoBehaviour
{
    public GameObject instructionScreen;
    
    void Awake()
    {
        instructionScreen.SetActive(false);
    }
    
    public void ToggleInstructionScreen()
    {
        instructionScreen.SetActive(!instructionScreen.activeSelf);
    }
}
