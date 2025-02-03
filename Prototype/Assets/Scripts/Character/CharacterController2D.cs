using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck; // Empty GameObject positioned at character's feet

    private Rigidbody2D rb;
    private CharacterAnimationSystem animationSystem;
    private bool isGrounded;
    private bool isRunning;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationSystem = GetComponent<CharacterAnimationSystem>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck transform is missing. Assign an empty GameObject at the feet.");
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow keys
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isRunning = runPressed;

        // Handle Movement
        if (moveInput != 0)
        {
            float speed = isRunning ? runSpeed : moveSpeed;
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
            animationSystem.SetAnimatorParameter("Speed", Mathf.Abs(moveInput * speed));

            animationSystem.ChangeState(isRunning ? CharacterState.Running : CharacterState.Walking);

            // Flip character direction
            if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
            {
                Flip();
            }
        }
        else
        {
            // Reset Speed and Animation when movement stops
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animationSystem.SetAnimatorParameter("Speed", 0f);
            animationSystem.ChangeState(CharacterState.Idle);
        }

        // Handle Jumping
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animationSystem.SetAnimatorParameter("VelocityY", jumpForce);
            animationSystem.SetAnimatorParameter("IsGrounded", false);
            animationSystem.ChangeState(CharacterState.Jumping);
        }
        else if (rb.linearVelocity.y < 0)
        {
            animationSystem.SetAnimatorParameter("VelocityY", rb.linearVelocity.y);
            animationSystem.ChangeState(CharacterState.JumpFall);
        }

        // Handle Landing
        if (isGrounded)
        {
            animationSystem.SetAnimatorParameter("IsGrounded", true);
        }
    }

    // Flip the character by inverting the local scale
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
