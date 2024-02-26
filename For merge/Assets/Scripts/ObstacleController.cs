using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Color obstacleColor;
    private PlayerColorChange playerColorChange;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        obstacleColor = GetComponent<SpriteRenderer>().color;
        playerColorChange = FindObjectOfType<PlayerColorChange>(); // Assuming there's only one PlayerColorChange script in the scene
    }

    void Update()
    {
        if (playerColorChange.GetColor() == obstacleColor)
        {
            // Matched colors, set collider size to the minimum
            boxCollider.size = new Vector2(0.0001f, 0.0001f); ;
        }
        else
        {
            // Colors don't match, set collider size to default
            boxCollider.size = new Vector2(1f, 1f); // Adjust this size according to your needs
        }
    }
}
