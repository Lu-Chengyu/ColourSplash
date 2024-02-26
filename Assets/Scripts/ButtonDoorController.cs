using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonDoorController : MonoBehaviour
{
    public SpriteRenderer buttonRenderer;
    public Color activeColor;
    public Color inactiveColor;
    public GameObject door;


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
            OpenDoor();
        }
        else
        {
            buttonRenderer.color = inactiveColor;
            CloseDoor();
        }
    }


    private void OpenDoor()
    {
        // Open the door (e.g., by rotating it or moving it up)
        door.SetActive(false);
    }


    private void CloseDoor()
    {
        // Close the door (e.g., by rotating it back or moving it down)
        door.SetActive(true);
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
