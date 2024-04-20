using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer circleRenderer;

    GameManager gameManager;
    Collider2D coll;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        circleRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            gameManager.UpdateCheckpointPosition(transform.position);
            circleRenderer.color = Color.white;
            coll.enabled = false;
        }
    }
}
