using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private PlayerColorChange playerColorChange; // Reference to the PlayerColorChange script

    private Vector2 velocity;
    private float inputAxis;

    public GameObject bulletObject;
    public float bullectSpeed = 5f;
    public float moveSpeed = 5f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 1.5f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool falling => velocity.y < 0f && !grounded;

    private bool canMove = true; // Flag to control movement constraint

    private void Awake()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerColorChange = GetComponent<PlayerColorChange>();
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        if (playerColorChange.GetColor() == Color.white)
        {
            canMove = false; // Stop movement
        }
        else
        {
            canMove = true; // Allow movement
        }

        if (canMove)
        {
            HorizontalMovement();
            ShootBullet();
            grounded = rigidbody.Raycast(Vector2.down);

            if (grounded)
            {
                GroundedMovement();
            }

            ApplyGravity();
        }
    }

    private void FixedUpdate()
    {
        // move mario based on his velocity
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        // clamp within the screen bounds
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        // accelerate / decelerate
        inputAxis = Input.GetAxis("Horizontal");
        float speedBuff = 1.0f;
        if (playerColorChange.GetColorName() == "Green" && Input.GetKeyDown(KeyCode.U))
        {

            speedBuff = 5.0f;
        }
        velocity.x = Mathf.MoveTowards(velocity.x * speedBuff, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);


        // check if running into a wall
        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
    }

    private void GroundedMovement()
    {
        // prevent gravity from infinitly building up
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        float jumpBuff = 1f;
        // perform jump
        if (Input.GetButtonDown("Jump"))
        {
            if (playerColorChange.GetColorName() == "Red" && Input.GetKey(KeyCode.U))
            {
                jumpBuff = 1.2f;
            }
            velocity.y = jumpForce * jumpBuff;
            jumping = true;
        }
    }

    private void ShootBullet()
    {
        if (playerColorChange.GetColorName() == "Blue" && Input.GetKeyDown(KeyCode.U))
        {
            GameObject bullet = Instantiate(bulletObject, rigidbody.position, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
        }
    }

    private void ApplyGravity()
    {
        // check if falling
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
            
        // apply gravity and terminal velocity
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // stop vertical movement if mario bonks his head
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

}
