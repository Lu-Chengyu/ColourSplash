using UnityEngine;

public class PlayerColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    public Color currentColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
        // randomInitColor();
        TryChangeColor(Color.red);
    }

    void Update()
    {
        // Check for keyboard inputs
        if (Input.GetKeyDown(KeyCode.J))
        {
            TryChangeColor(Color.red);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            TryChangeColor(Color.green);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            TryChangeColor(Color.blue);
        }
        else
        {
            return;
        }
    }

    void randomInitColor()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            ChangeColor(Color.red);
            
        }else if (random == 1)
        {
            ChangeColor(Color.green);
        }
        else
        {
            ChangeColor(Color.blue);
        }
    }
    void TryChangeColor(Color color)
    {
        // if (gameManager.IsColorAvailable(color) && color != spriteRenderer.color)
        if (color != spriteRenderer.color)
        {
            FindObjectOfType<AnalyticRecorder>().recordColorChange(ConvertColorName(currentColor), ConvertColorName(color), transform.position);
            ChangeColor(color);
            // gameManager.UpdateCounter(GetColorName(), -1); // Decrease counter
        }
    }

    void ChangeColor(Color newColor)
    {
        currentColor = newColor;
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

    public string ConvertColorName(Color color)
    {
        if (color == Color.red)
            return "Red";
        else if (color == Color.green)
            return "Green";
        else if (color == Color.blue)
            return "Blue";
        else
            return "";
    }
}
