using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    // Start is called before the first frame update
    // 0: R, 1: G, 2: B
    public int colorOption;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private PlayerColorChange playerColorChange;
    private Color obstacleColor;
    private Vector2 colliderSize;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        colliderSize = new Vector2(transform.localScale[0], transform.localScale[1]);
        colliderSize = boxCollider2D.size;
        boxCollider2D.offset = new Vector2(2.5f, 0.0f);
        setColor();
        playerColorChange = FindObjectOfType<PlayerColorChange>(); // Assuming there's only one PlayerColorChange script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (playerColorChange.GetColor() == obstacleColor)
        {
            // Matched colors, set collider size to the minimum
            boxCollider2D.size = new Vector2(0.0001f, 0.0001f); ;
        }
        else
        {
            // Colors don't match, set collider size to default
            boxCollider2D.size = new Vector2(colliderSize[0], colliderSize[1]); // Adjust this size according to your needs
        }
    }

    void setColor()
    {
        if (colorOption == 0)
        {
            spriteRenderer.color = Color.red;
        }else if (colorOption == 1)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }

        obstacleColor = spriteRenderer.color;
    }

    void fail()
    {
        Debug.Log("Bumped into different color! Fail!");
    }
    

}
