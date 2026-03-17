using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Misc Properties")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    [SerializeField] private Transform playerSprite;

    [Header("Player Parameters")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float gravityMultipler;
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float dashCooltime;

    [Header("Player Skills")]
    [SerializeField] private bool canAirDash;
    [SerializeField] private int jumpAddition;

    private Vector3 originalScale;
    private bool canAirJump;
    private bool applyAirJump;
    private int jumpCounter;
    private bool applyDash;
    private bool facingRight;
    private float originalGravity;
    private float jumpBufferTimer;
    private float coyoteTimer;
    private bool isGrounded;
    private float dashTimer;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        originalGravity = GetComponent<Rigidbody2D>().gravityScale;
        originalScale = playerSprite.localScale;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerDash();
        flipSprite();
    }

    private void FixedUpdate()
    {
        Calculations();
        jumpBufferTimer -= Time.fixedDeltaTime;

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
            applyAirJump = false;
            jumpCounter = 0;
        }

        else
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }
    }

    void PlayerMovement()
    {
        float xInput = Input.GetAxis("Horizontal") * playerSpeed;
        movement = Vector2.right * xInput;

    }

    void PlayerDash()
    {
        dashTimer -= Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse2) && isGrounded && dashTimer <= 0)
        {
            dashTimer = dashCooltime;
            applyDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && canAirDash && dashTimer <=0)
        {
            dashTimer = dashCooltime;
            applyDash = true;
        }
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
            rb.gravityScale = originalGravity;

        }

        if(Input.GetButtonDown("Jump") && jumpCounter != jumpAddition)
        {
            applyAirJump = true;
            rb.gravityScale = originalGravity;
        }

        jumpBufferTimer -= Time.fixedDeltaTime;
    

        if (rb.linearVelocityY < 0f)
        {
            rb.gravityScale *= gravityMultipler;
            
            if(rb.gravityScale >= maxFallSpeed)
            {
                rb.gravityScale = maxFallSpeed;
            }

        }

        if (Input.GetButton("Jump"))
        {
            if (rb.linearVelocityY < 0f)
            {
                rb.gravityScale = floatingSpeed;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rb.linearVelocityY < 0f)
            {
                rb.gravityScale = maxFallSpeed;
                
            }
        }

    }

    void flipSprite()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (xInput > 0)
        {
            var xFlip = originalScale.x;
            playerSprite.localScale = new Vector3(xFlip, playerSprite.localScale.y, playerSprite.localScale.z);
            facingRight = true;
        }

        if (xInput < 0)
        {
            var xFlip = originalScale.x * -1;
            playerSprite.localScale = new Vector3(xFlip, playerSprite.localScale.y, playerSprite.localScale.z);
            facingRight = false;
        }
       
    }
    
    void Calculations()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, circleRadius,groundLayer);
        
        rb.AddForce(movement, ForceMode2D.Force);

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
            applyAirJump = false;
            jumpCounter = 0;
            rb.gravityScale = originalGravity;
        }

        else
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }

        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }

        if (applyDash)
        {
            var dash = (facingRight) ? Vector2.right * dashSpeed : -Vector2.right * dashSpeed;                                                              
            rb.AddForce(dash, ForceMode2D.Impulse);
            applyDash = false;
        }


        if (applyAirJump)
        {
            jumpCounter++;
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            applyAirJump = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
}
