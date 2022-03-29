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
    [Header("Player State")]
    public bool isCrouch = false;
    public bool isOnGround;
    public bool isJump;

    [Header("Environment Check")]
    public LayerMask groundLayer;

    float xVelocity;

    [Header("Jump")]
    public float jumpForc = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float crouchJumpBoost = 2.5f;
    float jumpTime;
    
    //box collider size
    Vector2 colliderStandSize;
    Vector2 colliderstandOffsize;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        boxColl = GetComponent<BoxCollider2D>();
        colliderStandSize = boxColl.size;
        colliderstandOffsize = boxColl.offset;

        colliderCrouchSize = new Vector2(boxColl.size.x, boxColl.size.y/2);
        colliderCrouchOffset = new Vector2(boxColl.offset.x, boxColl.offset.y/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        GroundMovement();
    }

    void GroundMovement(){

        if(Input.GetButton("Crouch"))
            Crouch();
        else if(!Input.GetButton("Crouch") && isCrouch)
            StandUp();


        xVelocity = Input.GetAxis("Horizontal");
        FlipDirection();

        if(isCrouch)
            xVelocity /= crouchSpeedDivisor;

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

    }

    void FlipDirection(){
        if(xVelocity <0)
            transform.localScale = new Vector2(-1,1);
        if(xVelocity >0)
            transform.localScale = new Vector2(1,1);
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

}
