using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        // Check for keyboard inputs
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryChangeColor(Color.red);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            TryChangeColor(Color.green);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            TryChangeColor(Color.blue);
        }
    }

    void TryChangeColor(Color color)
    {
        if (gameManager.IsColorAvailable(color) && color != spriteRenderer.color)
        {
            ChangeColor(color);
            gameManager.UpdateCounter(GetColorName(), -1); // Decrease counter
        }
    }

    void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public Color GetColor()
    {
        Color color = spriteRenderer.color;
        return color;
    }

    public string GetColorName()
    {
        if (spriteRenderer.color == Color.red)
            return "Red";
        else if (spriteRenderer.color == Color.green)
            return "Green";
        else if (spriteRenderer.color == Color.blue)
            return "Blue";
        else
            return "";
    }
}
