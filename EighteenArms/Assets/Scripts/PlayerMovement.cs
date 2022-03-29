using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxColl;

    [Header("Movement")]
    public float speed = 0;
    public float crouchSpeedDivisor = 3f;
        
    [Header("Jump")]
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float crouchJumpBoost = 2.5f;
    public float hangingJumpForce = 15f;
    float jumpTime;

    [Header("Player State")]
    public bool isCrouch = false;
    public bool isOnGround;
    public bool isJump;
    public bool isHeadBlock;
    public bool isHanging;

    [Header("Environment Check")]
    public LayerMask groundLayer;
    public float footOffset = 0.365f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    public Transform leftFootSensor;
    public Transform rightFootSensor;

    float playerHeight;
    public float eyeHeight = 1.1f;
    public float grabDistance = 0.4f;
    public float reachOffset = 0.1f;


    public float xVelocity;

    //keyPressed
    public bool jumpPressed;
    public bool jumpHeld;
    public bool crouchHeld;
    public bool crouchPressed;

    
    //box collider size
    Vector2 colliderStandSize;
    Vector2 colliderstandOffsize;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        boxColl = GetComponent<BoxCollider2D>();
        colliderStandSize = boxColl.size;
        colliderstandOffsize = boxColl.offset;

        colliderCrouchSize = new Vector2(boxColl.size.x, boxColl.size.y/2);
        colliderCrouchOffset = new Vector2(boxColl.offset.x, boxColl.offset.y/2);

        playerHeight = boxColl.size.y;
    }

    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
        crouchPressed = Input.GetButtonDown("Crouch");
    }
    private void FixedUpdate() {
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck() {

        //Vector2 footPostion = footSensor.position;
        //Vector2 offset = new Vector2(0, 0.1f);

        //RaycastHit2D footCheck = Physics2D.Raycast(footPostion + offset, Vector2.down, groundDistance, groundLayer);
        //Debug.DrawRay(footPostion + offset, Vector2.down, Color.red, groundDistance);
        
        //left, right foots ray
        RaycastHit2D lFootCheck = Raycast(leftFootSensor.position, new Vector2(0f, 0.1f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rFootCheck = Raycast(rightFootSensor.position, new Vector2(0f, 0.1f), Vector2.down, groundDistance, groundLayer);

        if (lFootCheck || rFootCheck)
        {
            isOnGround = true;
        }
        else
            isOnGround = false;


        //head above ray
        RaycastHit2D headCheck = Raycast(transform.position, new Vector2(0,boxColl.size.y), Vector2.up, headClearance, groundLayer);
        if (headCheck)
        {
            isHeadBlock = true;
        }
        else
            isHeadBlock = false;

        float direction = transform.localScale.x;
        Vector2 grabDir = new Vector2(direction, 0);
        RaycastHit2D blockedCheck = Raycast(rightFootSensor.position, new Vector2(0f, playerHeight + 0.1f), grabDir, grabDistance, groundLayer);
        RaycastHit2D wallCheck = Raycast(rightFootSensor.position, new Vector2(0f, eyeHeight), grabDir, grabDistance, groundLayer);
        RaycastHit2D ledgeCheck = Raycast(rightFootSensor.position, new Vector2(reachOffset*direction, playerHeight + 0.1f), Vector2.down, grabDistance, groundLayer);

        if (!isOnGround && rb.velocity.y < 0 && ledgeCheck && wallCheck && !blockedCheck)
        {

            Vector3 pos = transform.position;
            pos.x += (wallCheck.distance - 0.05f)*direction;
            pos.y -= ledgeCheck.distance;

            transform.position = pos;

            rb.bodyType = RigidbodyType2D.Static;
            isHanging = true;
        }
    }

    void GroundMovement(){
        if (isHanging)
            return;

        if (crouchHeld && !isCrouch && isOnGround)
            Crouch();
        else if (!crouchHeld && isCrouch && !isHeadBlock)
            StandUp();
        else if (!isOnGround && isCrouch)
            StandUp();


        xVelocity = Input.GetAxis("Horizontal");
        FlipDirection();

        if(isCrouch)
            xVelocity /= crouchSpeedDivisor;

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

    }

    void FlipDirection(){
        if(xVelocity <0)
            transform.localScale = new Vector3(-1,1,1);
        if(xVelocity >0)
            transform.localScale = new Vector3(1,1,1);
    }

    void Crouch(){
        isCrouch = true;
        boxColl.offset = colliderCrouchOffset;
        boxColl.size = colliderCrouchSize;
    }

    void StandUp(){
        isCrouch = false;
        boxColl.offset = colliderstandOffsize;
        boxColl.size = colliderStandSize;
    }

    void MidAirMovement() {

        if (isHanging) {
            if (jumpPressed) {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(rb.velocity.x, hangingJumpForce);
                isHanging = false;
            }

            if (crouchPressed) {
                rb.bodyType = RigidbodyType2D.Dynamic;
                isHanging = false;
            }
        }

        if (isOnGround && jumpPressed && !isJump && !isHeadBlock)
        {
            if (isCrouch) {
                StandUp();
                rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            isOnGround = false;
            isJump = true;
            jumpTime = Time.time + jumpHoldDuration;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        else if (isJump) {
            if (jumpHeld) {
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            }
            if (jumpTime < Time.time) {
                isJump = false;
            }
        }
    }

    RaycastHit2D Raycast(Vector2 pos, Vector2 offset, Vector2 rayDirection, float distance, LayerMask layer) {
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, distance, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection, color, distance);
        return hit;
    }

}
