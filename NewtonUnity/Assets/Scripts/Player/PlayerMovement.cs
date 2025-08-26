using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ──────────────── Assignables ────────────────
    public Transform playerCam;
    public Transform orientation;
    public Rigidbody rb;

    // ──────────────── Rotation and look ────────────────
    private float xRotation;
    private float sensitivity = 70f;
    private float sensMultiplier = 1f;

    // ──────────────── Movement ────────────────
    public float moveSpeed = 2000;
    public float maxSpeed = 10;
    public bool grounded;
    public LayerMask whatIsGround;
    public float startMoveSpeed;
    public float startMaxSpeed;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    // ──────────────── Crouching ────────────────
    public GameObject playerBody;
    public Vector3 startScale;
    public float crouchAmount;
    private bool isCrouching = false;
    public GameObject crouchingGraphic;
    public GameObject standingGraphic;

    // ──────────────── Jumping ────────────────
    private bool readyToJump = true;
    private float jumpCooldown = 0.65f;
    public float jumpForce = 150f;
    private bool isOnGround = true;

    // ──────────────── Input ────────────────
    float x, y;
    bool jumping, sprinting, crouching;

    // ──────────────── Sliding / Surface normals ────────────────
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    // ──────────────── Gravity ────────────────
    public float GravityAmount = -10;
    public bool forceDrop;

    // ──────────────── Unity Methods ────────────────
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Stores the starting speed values
        startMaxSpeed = maxSpeed;
        startMoveSpeed = moveSpeed;
        startScale = this.transform.localScale;

        // Can't see mouse and stays in the middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        standingGraphic.SetActive(true);
        crouchingGraphic.SetActive(false);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        MyInput();
        Look();

        // Toggle crouch method
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                startCrouch();
            }
            else
            {
                isCrouching = false;
                stopCrouch();
            }
        }

        // PRESS AND HOLD crouch method (disabled)
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     isCrouching = true;
        //     startCrouch();
        // }
        // else if (Input.GetKeyUp(KeyCode.LeftShift))
        // {
        //     isCrouching = false;
        //     stopCrouch();
        // }
    }

    // ──────────────── Input ────────────────
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
    }

    // ──────────────── Movement ────────────────
    private void Movement()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * GravityAmount);

        // Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        // Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        // If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        // Set max speed
        float maxSpeed = this.maxSpeed;
        if (isCrouching)
            maxSpeed = this.maxSpeed / 2;

        // Prevent exceeding max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        if (isCrouching)
            multiplier = 0.5f;

        // Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    // ──────────────── Crouch ────────────────
    private void startCrouch()
    {
        playerBody.transform.localScale = new Vector3(startScale.x, crouchAmount, startScale.z);
        standingGraphic.SetActive(false);
        crouchingGraphic.SetActive(true);
    }

    private void stopCrouch()
    {
        playerBody.transform.localScale = startScale;
        crouchingGraphic.SetActive(false);
        standingGraphic.SetActive(true);
    }

    // ──────────────── Jump ────────────────
    private void Jump()
    {
        if (grounded && readyToJump && isOnGround == true)
        {
            readyToJump = false;
            isOnGround = false;

            // Add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // If jumping while falling, reset y velocity
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    // ──────────────── Look ────────────────
    private float desiredX;

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // Rotate, clamped
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    // ──────────────── Counter Movement ────────────────
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        // Counter movement X
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f ||
            (mag.x < -threshold && x > 0) ||
            (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }

        // Counter movement Y
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f ||
            (mag.y < -threshold && y > 0) ||
            (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        // Limit diagonal running
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    // ──────────────── Helpers ────────────────
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    // ──────────────── Collision Handling ────────────────
    private bool cancellingGrounded;

    private void OnCollisionStay(Collision other)
    {
        // Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            // FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // Invoke ground/wall cancel
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Doesn't allow double jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("confinedGround"))
        {
            forceDrop = true;
        }
    }
}
