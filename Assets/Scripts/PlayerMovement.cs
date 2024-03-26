using System.Collections;
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
    // public float bullectSpeed = 5f;
    public Vector2 moveDirection = Vector2.right;  // movement direction
    public float moveSpeed = 5f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 1.5f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public RaycastHit2D grounded { get; private set; }
    public LayerMask groundLayer;
    public bool jumping { get; private set; }
    public bool falling => velocity.y < 0f && !grounded;

    private bool canMove = true; // Flag to control movement constraint
    // private bool isSkillCoolDown = false;

    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f; 
    private bool isDashing = false; 
    private float dashEndTime = 0f; 
    private float dashCoolDown = 2f; 
    private float lastDashTime = -10f;

    private void Awake()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerColorChange = GetComponent<PlayerColorChange>();
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
            // grounded = rigidbody.Raycast(Vector2.down);
            grounded = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f,groundLayer);
            if (grounded)
            {
                // Debug.Log(grounded.collider.name);
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
        if (!isDashing && playerColorChange.GetColorName() == "Green" && Input.GetKeyDown(KeyCode.U) && Time.time > lastDashTime + dashCoolDown)
        {
            StartDash();
            FindObjectOfType<Cooldown>().StartCooldown();
        }

        if (isDashing)
        {
            // If a dash is in progress, the speed is set directly in the StartDash method
            if (Time.time >= dashEndTime)
            {
                EndDash();
            }
            return; // If dashing, no other inputs are processed
        }

        inputAxis = Input.GetAxisRaw("Horizontal");
        if (inputAxis > 0)
        {
            if (moveDirection == Vector2.left)
            {
                FlipPlayer();
            }
            moveDirection = Vector2.right;
        }
        else if (inputAxis < 0)
        {
            if (moveDirection == Vector2.right)
            {
                FlipPlayer();
            }
            moveDirection = Vector2.left;
        }

        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, 1f);

        // Stop moving if the character is facing a wall
        if (rigidbody.Raycast(Vector2.right * Mathf.Sign(velocity.x)))
        {
            velocity.x = 0f;
        }

        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
    }

    private void StartDash()
    {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        lastDashTime = Time.time;
        velocity.x = moveDirection.x * dashSpeed; 
    }

    private void EndDash()
    {
        isDashing = false;
        // The sprint ends and the velocity logic will be recalculated in the
        // HorizontalMovement of the next Update cycle
    }

    private void FlipPlayer()
    {
        // transform.Rotate(0, 0, 180);
        Vector3 currentScale = transform.localScale;
        currentScale.x = currentScale.x * -1f;
        transform.localScale = currentScale;
    }

    private void GroundedMovement()
    {
        // prevent gravity from infinitly building up
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        // perform jump
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping!");
            velocity.y = jumpForce;
            jumping = true;
        }
        if (playerColorChange.GetColorName() == "Red" && Input.GetKeyDown(KeyCode.U))
        {
            float jumpBuff = 1.7f;
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
            // bullet.AddComponent<BulletController>();
            if (bc != null)
            {
                bc.moveDirection = moveDirection;
                bc.DestroyBullet();
            }
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
