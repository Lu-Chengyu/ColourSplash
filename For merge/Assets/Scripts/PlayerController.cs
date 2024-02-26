using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    
    public Transform groundCheck;
    public GameObject redCounterText;
    public GameObject greenCounterText;
    public GameObject blueCounterText;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool canChangeColor = true;
    private int redCounter = 0;
    private int greenCounter = 0;
    private int blueCounter = 0;
    private Color currentColor = Color.white;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateCounters();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.E)) // Assuming 'E' is the key to interact
        {
            PushBox();
        }
        
        if (canChangeColor)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeColor(Color.red);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                ChangeColor(Color.green);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeColor(Color.blue);
            }
        }
        
    }

    void ChangeColor(Color color)
    {
        if (currentColor != color)
        {
            currentColor = color;
            if (color == Color.red)
            {
                redCounter--;
            }
            else if (color == Color.green)
            {
                greenCounter--;
            }
            else if (color == Color.blue)
            {
                blueCounter--;
            }
            UpdateCounters();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            CollectCollectible(other.gameObject.GetComponent<Collectible>().color);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (other.GetComponent<Obstacle>().color == currentColor)
            {
                // Player can pass through this obstacle
            }
            else
            {
                // Handle collision with obstacle (e.g., restart game)
            }
        }
        else if (other.CompareTag("Box"))
        {
            PushBox();
        }
        else if (other.CompareTag("Pit"))
        {
            if (other.GetComponent<Pit>().color == currentColor)
            {
                // Player falls into the pit, restart game
            }
        }
    }

    void CollectCollectible(Color color)
    {
        if (color == Color.red)
        {
            redCounter++;
        }
        else if (color == Color.green)
        {
            greenCounter++;
        }
        else if (color == Color.blue)
        {
            blueCounter++;
        }
        UpdateCounters();
    }
    
    void PushBox()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            Box box = hit.collider.GetComponent<Box>();
            if (box.CanInteract(currentColor))
            {
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.Move(Vector2.right * transform.localScale.x, 500f); // Adjust force as needed
            }
        }
    }
    
    

    void UpdateCounters()
    {
        redCounterText.GetComponent<TextMesh>().text = "Red: " + redCounter;
        greenCounterText.GetComponent<TextMesh>().text = "Green: " + greenCounter;
        blueCounterText.GetComponent<TextMesh>().text = "Blue: " + blueCounter;
    }
}