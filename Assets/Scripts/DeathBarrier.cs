using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private GameManager gameManager;
    private Color pitColor;
    private PlayerColorChange playerColorChange;
    private bool playerInside = false;

    void Start()
    {
        gameManager = GameManager.Instance;
        pitColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (playerInside && playerColorChange != null && playerColorChange.GetColor() != pitColor)
        {
            playerColorChange.gameObject.SetActive(false); // Turn off the player
            gameManager.DestroyGameInstance(); // Destroy the game instance
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            playerColorChange = other.GetComponent<PlayerColorChange>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerColorChange = null;
        }
    }
}
