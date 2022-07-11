using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D: MonoBehaviour
{
    private const float gravityScaleJumping = 3.5f;
    private const float gravityScaleFalling = 5f;


    [SerializeField] private float speed=50;
    [SerializeField] private Vector2 jumpForce;
    
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheckObject;
    [SerializeField] LayerMask groundLayerMask;

    //In case player movement needs to be locked while jumping
    [SerializeField] private bool isAllowedAirControl = true;

    private bool isGrounded = true;
    private bool hasDoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
    }

    private void FixedUpdate()
    {
        
    }

    public void move(float movementDirection, bool wantsToJump)
    {
        //Horizontal Movement
        if (isGrounded || isAllowedAirControl)
        {
            Vector2 movement = new Vector2(movementDirection, 0);
            movement *= speed;
            rigidBody.AddForce(movement);
        }
        //Jump
        if ((isGrounded || hasDoubleJump) && wantsToJump)
        {
            rigidBody.gravityScale = gravityScaleJumping;
            rigidBody.velocity -= new Vector2 (0f, rigidBody.velocity.y);
            rigidBody.AddForce(jumpForce);
            if (!isGrounded)
                hasDoubleJump = false;
        }
        //Changes gravity back to normal at the peak of player's jump
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.gravityScale = gravityScaleFalling;
        }
    }

    private void groundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckObject.position, .1f, groundLayerMask);

        if (isGrounded)
            hasDoubleJump = true;
    }
}
