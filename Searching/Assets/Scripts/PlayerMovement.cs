using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float drag = 10f;
    [SerializeField] private float maxSpeed = 50f; // Maximum travel speed to avoid rigidbody velocity insanity
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float currentSpeed;

    //[Header("Stamina")]
    //[SerializeField] private StaminaBar staminaBar;
    //[SerializeField] private float staminaAmount;
    //[SerializeField] private float maxStamina;
    //[SerializeField] private float sprintStamCost;
    //[SerializeField] private float slideStamCost;
    //[SerializeField] private float stamRechargeAmount;

    [Header("Slope")]
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit onSlopeHit;
    private bool exitingSlope;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchYScale;
    [SerializeField] private float startYScale;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] public MouseLook mouseLook = new MouseLook();
    Rigidbody rb;
    Vector2 input;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    public bool groundCheck;

    [Header("Sliding")]
    [SerializeField] private float slideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideTimer;
    [SerializeField] private float slideYScale;

    bool forceApplied = false;
    [SerializeField] private bool sprinting = false;
    [SerializeField] private bool crouching = false;
    [SerializeField] private bool sliding = false;
    // Vector2 for player input
    Vector2 GetInput()
    {
        Vector2 input = new Vector2
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
        return input;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
        startYScale = transform.localScale.y;
        //staminaBar.SetMaxStamina(maxStamina);
        // Call of mouseLook to allow for initilisation of the camera/mouse movement;
        mouseLook.Init(transform, cam.transform);
        //staminaBar.HideUI();
    }

    void Update()
    {
        // The seeence of this update function is that GroundCheck() makes sure the player is grounded, PlayerAcceleration controls the rigidbody velocity so that the player doesn't go flying, 
        // and then there's a couple of grapple conditions to make sure you can't jump, move your mouse or groundslam while grappling
        GroundCheck();
        PlayerAcceleration();
        PlayerRotation();
        PlayerMove();
        //staminaBar.SetStamina(staminaAmount);
        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            PlayerJump();
        }

        if (Input.GetButton("Crouch") && groundCheck)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            if (crouching == false)
            {
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            crouching = true;
            currentSpeed = crouchSpeed;
            
        }

        if (Input.GetButtonUp("Crouch") && groundCheck)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            crouching = false;
            currentSpeed = movementSpeed;
        }

        //if (sprinting || sliding)
        //{
        //    staminaBar.ShowUI();
        //}

        //if (!sprinting && !sliding)
        //{
        //    RechargeStamina();
        //}
    }

    //GroundCheck does exactly as the name implies, makes sure that the player is grounded, and if they are to imply drag so they don't zip about the place while on the ground
    void GroundCheck()
    {
        groundCheck = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f + 0.2f));
        if (groundCheck)
        {
            rb.drag = drag;
            forceApplied = false;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    void PlayerMove()
    {
        // Calls input, then gets the Vector3 from said input and applies it as a rigidbody, with 10f making sure there's an actual multiplier to that movement so the thing actually moves
        input = GetInput();
        Vector3 movementDir = transform.forward * input.y + transform.right * input.x;

        //if (OnSlope())
        //{
        //    rb.AddForce(GetSlopeDirection() * movementSpeed * 20f, ForceMode.Force);
        //}

        if (!sliding)
        {
            rb.AddForce(movementDir.normalized * currentSpeed * 10f, ForceMode.Force);
        }

        //if ((Input.GetButton("Sprint") && !crouching))
        //{
        //    PlayerSprint();
        //}
        
        //if ((Input.GetButtonUp("Sprint") && !crouching) || (staminaAmount <= 0))
        //{
        //    sprinting = false;
        //    currentSpeed = movementSpeed;
        //}
    }

    void PlayerJump()
    {
        // Jump is really simple! It just resets velocity and then adds force so that they can jump into the air
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
    }
    
    //void PlayerSprint()
    //{
    //    if (staminaAmount <= 0) return;
    //    sprinting = true;
    //    currentSpeed = sprintSpeed;
    //    staminaAmount -= sprintStamCost;   
    //}

    //void RechargeStamina()
    //{
    //    if (staminaAmount == maxStamina)
    //    {
    //        staminaBar.HideUI();
    //        return;
    //    }
    //    staminaAmount += stamRechargeAmount;
    //    if (staminaAmount > maxStamina)
    //    {
    //        staminaAmount = maxStamina;
    //    }
    //}

    void PlayerRotation()
    {
        // PlayerRotation is just a call of mouseLook.LookRotation
        mouseLook.LookRotation(transform, cam.transform);
    }

    void PlayerAcceleration()
    {
        // Because rigidbody velocities are a pain, PlayerAcceleration was built to limit momentum, especially during things like dashes which if done in the air without a restriction would
        // send someone flying, hence why there's a check for the dash to change max acceleration for.
        float currentMaxSpeed = maxSpeed;
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (velocity.magnitude > currentMaxSpeed)
        {
            Vector3 maxVelocity = velocity.normalized * currentMaxSpeed;
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out onSlopeHit, playerHeight * 0.5f + 0.2f))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, onSlopeHit.normal);
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeDirection()
    {
        return Vector3.ProjectOnPlane(input, onSlopeHit.normal).normalized;
    }
}
