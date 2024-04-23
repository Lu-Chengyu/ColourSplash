using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer circleRenderer;

    GameManager gameManager;
    Collider2D coll;
    public GameObject checkpointText;
    public Vector3 playerInitPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        circleRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        checkpointText = gameManager.ckptText;
        playerInitPosition = gameManager.player.position;
    }

    private void Update()
    {
        Debug.Log(gameManager.player.position);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Reach Checkpoint");
            gameManager.UpdateCheckpointPosition(transform.position);
            // gameManager.UpdateCheckpointPosition(collision.gameObject.transform.position);
            PlayerPrefs.SetInt("reachCheckpoint", 1);
            PlayerPrefs.Save();
            circleRenderer.color = Color.white;
            coll.enabled = false;
            showText();
        }
    }
    
    private void showText()
    {
        StartCoroutine(ShowUIText());
    }
    
    private IEnumerator ShowUIText()
    {
        checkpointText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        checkpointText.gameObject.SetActive(false);
    }
}
