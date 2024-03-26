using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonDoorController : MonoBehaviour
{
    public SpriteRenderer buttonRenderer;
    public Color activeColor;
    public Color inactiveColor;
    public GameObject[] doors;


    //private bool isWeightOnButton = false;
    //private int playerCount = 0;

    private bool isDoorOpen = false;


    //private void Update()
    //{
    //    if (isWeightOnButton)
    //    {
    //        buttonRenderer.color = activeColor;
    //        OpenDoor();
    //    }
    //    else
    //    {
    //        buttonRenderer.color = inactiveColor;
    //        CloseDoor();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDoorOpen = !isDoorOpen;
            UpdateButtonAndDoor();
        }
    }

    private void UpdateButtonAndDoor()
    {
        if (isDoorOpen)
        {
            buttonRenderer.color = activeColor;
            OpenDoors();
        }
        else
        {
            buttonRenderer.color = inactiveColor;
            CloseDoors();
        }
    }


    private void OpenDoors()
    {
        // Open the door (e.g., by rotating it or moving it up)
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }


    private void CloseDoors()
    {
        // Close the door (e.g., by rotating it back or moving it down)
         foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerCount++;
    //        isWeightOnButton = true;
    //    }
    //}


    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerCount--;
    //        if (playerCount == 0)
    //        {
    //            isWeightOnButton = false;
    //        }
    //    }
    //}
}
