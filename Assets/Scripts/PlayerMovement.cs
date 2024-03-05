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

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool falling => velocity.y < 0f && !grounded;

    private bool canMove = true; // Flag to control movement constraint
    private bool isSkillCoolDown = false;

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
            //Debug.Log(grounded);
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
        if (inputAxis > 0)
        {
            moveDirection = Vector2.right;
        }
        else if (inputAxis < 0)
        {
            moveDirection = Vector2.left;
        }
        float speedBuff = 1.0f;
        if (playerColorChange.GetColorName() == "Green" && Input.GetKeyDown(KeyCode.U) && !isSkillCoolDown)
        {
            speedBuff = 6.0f;
            isSkillCoolDown = true;
            StartCoroutine(SkillCooldownRoutine());
        }
        // velocity.x = Mathf.MoveTowards(velocity.x * speedBuff, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        velocity.x = Mathf.MoveTowards(velocity.x * speedBuff, inputAxis * moveSpeed * speedBuff, 1f);


        // check if running into a wall
        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
        
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
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
        else if (playerColorChange.GetColorName() == "Red" && Input.GetKeyDown(KeyCode.U))
        {
            float jumpBuff = 1.65f;
            velocity.y = jumpForce * jumpBuff;
            jumping = true;
        }
    }
    private IEnumerator SkillCooldownRoutine()
    {
        float startTime = Time.time;
        while (isSkillCoolDown && Time.time - startTime < 2f)
        {
            yield return null; // Wait for next frame
        }

        isSkillCoolDown = false; // Skill is ready after cooldown
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
