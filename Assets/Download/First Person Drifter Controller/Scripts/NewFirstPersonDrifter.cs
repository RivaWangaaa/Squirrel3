// original by Eric Haines (Eric5h5)
// http://wiki.unity3d.com/index.php/FPSWalkerEnhanced

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (CharacterController))]
public class NewFirstPersonDrifter: MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
 
    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    private bool limitDiagonalSpeed = true;
 
    public bool enableRunning = true;
 
    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;
 
    // Units that player can fall before a falling damage function is run. To disable, type "infinity" in the inspector
    private float fallingDamageThreshold = 10.0f;
 
    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;
 
    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;
 
    public float slideSpeed = 5.0f;
 
    // If checked, then the player can change direction while in the air
    public bool airControl = true;
 
    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;
 
    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;
 
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CharacterController controller;
    private Transform myTransform;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private int jumpTimer;
    
    //Yanxi: for detecting long-press SPACE key
    /*
    public float ClickDuration = 0.5f;
    private float totalDownTime = 0;
    private bool clicking = false;
    private float extraJump = 0f;
    */
    
    //Yanxi: the amount of lower gravity and normal gravity to control the max height & jump speed of a jump
    public float lowGravity = 5;
    public float normalGravity = 12;
    
    //Yanxi: for crouching
    public float crouchHeight = 0.5f;
    private bool crouching = false;
    public float crouchDown = 0.5f;
    private Vector3 crouchPosition;
    private Vector3 standPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
        
    }
    
    // Yanxi: for continuous detection of pressing crouch key
   void Update()
    {
        if (grounded)
        {
            //Yanxi: Crouch
            if (Input.GetKeyDown("c"))
            {
                Debug.Log("Crouching");
                /*
                crouchPosition = new Vector3(transform.position.x, transform.position.y - crouchHeight,
                    transform.position.z);
                    */
                
                
                if (crouching)
                {
                    crouching = false;
                }
                else
                {
                    crouching = true;
                }

                if (crouching)
                {

                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - crouchDown,
                        transform.localScale.z);
                    
                    //transform.position = Vector3.Lerp(transform.position, crouchPosition, Time.deltaTime);
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + crouchDown,
                        transform.localScale.z);
                    
                    /*
                    standPosition = new Vector3(transform.position.x, transform.position.y + crouchHeight,
                        transform.position.z);
                    
                    transform.position = Vector3.Lerp(transform.position,standPosition,
                        Time.deltaTime);
                        */
                }
            }
        }
    }

    void FixedUpdate()
    {

        if (Player.isStop)
            return;
        
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed)? .7071f : 1.0f;
 
        //Yanxi: when player release SPACE, change gravity to normal; (but this function doesn't trigger frequently enough)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            KeyReleased();
            Debug.Log("SPACE Released");
        }
        
        //Yanxi: when player is not pressing SPACE, change gravity to normal; (this function trigger more frequently)
        if (!Input.GetKey(KeyCode.Space))
        {
            KeyReleased();
            Debug.Log("Not Holding SPACE");
        }
        
        if (grounded) {
            bool sliding = false;
            // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
            // because that interferes with step climbing amongst other annoyances
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance)) {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
            // However, just raycasting straight down from the center can fail when on steep slopes
            // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
            else {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
 
            // If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
            if (falling) {
                falling = false;
                if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                    FallingDamageAlert (fallStartLevel - myTransform.position.y);
            }
 
            if( enableRunning )
            {
                //Yanxi: change [Input.GetButton("Run")] to [Input.GetKey(KeyCode.LeftShift)]
                //Yanxi: because 'Run' button isn't assigned, and I couldn't find where to assign it
            	speed = Input.GetKey(KeyCode.LeftShift)? runSpeed : walkSpeed;
                Debug.Log("Moving Speed: " + speed);
            }
 
            // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
            if ( (sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide") ) {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize (ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            else {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }
            
            //Yanxi
            //LongPressDetect();

            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            if (!Input.GetButton("Jump"))
            {
                jumpTimer++;
            }
            
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                //Yanxi: lower gravity when start jumping
                HighJump();
                
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
                
            }

        }
        else {
            // If we stepped over a cliff or something, set the height at which we started falling
            if (!falling) {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }
 
            // If air control is allowed, check movement but don't touch the y component
            if (airControl && playerControl) {
                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }
        
        
         // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
 
        // Move the controller, and set grounded true or false depending on whether we're standing on something
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }
 
    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit (ControllerColliderHit hit) {
        contactPoint = hit.point;
    }
 
    // If falling damage occured, this is the place to do something about it. You can make the player
    // have hitpoints and remove some of them based on the distance fallen, add sound effects, etc.
    void FallingDamageAlert (float fallDistance)
    {
        //print ("Ouch! Fell " + fallDistance + " units!");   
    }
    
    //Yanxi: Detect if it's a long-press SPACE or a short-press SPACE
    /*
    void LongPressDetect()
    {
        //Detect the first click
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pressed");
            totalDownTime = 0;
            clicking = true;
        }
        
        //If a first click detected, and still clicking, measure the total click time
        //if we exceed the duration specified
        if (clicking && Input.GetKey(KeyCode.Space))
        {
            totalDownTime += Time.deltaTime;

            if (totalDownTime >= ClickDuration)
            {
                Debug.Log("Long Press");
                clicking = false;
            }
            
        }
        
        //If a first click detected, and we release before the duration, do nothing, just cancel the click
        if (clicking && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Short Press");
            clicking = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            KeyReleased();
        }

    }
    */
    
    //Yanxi: change gravity lower for higher jump
    void HighJump()
    {
        gravity = lowGravity;
    }
    
    //Yanxi: when SPACE is released, change gravity back to normal and players start to fall
    void KeyReleased()
    {
        gravity = normalGravity;
    }
}