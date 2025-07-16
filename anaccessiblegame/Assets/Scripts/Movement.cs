using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public float maxMoveSpeed = 5;
    [SerializeField] float acceleration = 5;
    [Space]
    public float sprint_maxMoveSpeed = 10;
    [SerializeField] float sprint_acceleration = 10;
    [Space]
    [SerializeField] float deceleration = 5;
    [Tooltip("multiplier applied to acceleration when player is in the air")]
    [SerializeField] float midairAccelMult = .5f;

    // direction of actual movement
    public float xVelocity { get; private set; } = 0;
    // direction of input
    int moveDirection = 0;
    float moveInputStrength = 1;

    public bool isSprinting { get; private set; } = false;

    Rigidbody2D rb2d;
    [SerializeField] Grounded groundedScript;

    bool canMove = true;

    Accessibility accessibility;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        accessibility = Accessibility.Instance;

        // sprinting stops when toggleRun is turned off
        accessibility.toggleRunDisabled.AddListener(ResetSprinting);
    }

    public void Move(int moveDirection)
    {
        // do nothing if movement is disabled
        if (!canMove) { return; }

        this.moveDirection = moveDirection;

        // stop time if movement input is 0, resume if there is any movement input
        if (accessibility.timeStop)
        {
            if (moveDirection == 0) { Time.timeScale = 0; }
            else { Time.timeScale = accessibility.gameSpeedPercent / 100f; }
        }
    }

    public void Sprint(bool isSprinting)
    {
        // do nothing if movement is disabled
        if (!canMove) { return; }

        // note on detecting if run button is being clicked:
        // isSprinting is true when button is clicked because the button calls this method with its clicked t/f bool


        // if toggleRun is on and run button is being clicked,
        if (accessibility.toggleRun && isSprinting == true)
        {
            // toggle the player's sprinting status
            if (this.isSprinting) { this.isSprinting = false; }
            else { this.isSprinting = true; }
        }

        // if toggleRun is not on, do default behavior
        // default behavior: sprint on click, stop sprinting on release
        else if (!accessibility.toggleRun) { this.isSprinting = isSprinting; }
    }

    /// <summary>
    /// stops running. used to reset sprinting status when toggleSprint is disabled
    /// </summary>
    void ResetSprinting()
    {
        isSprinting = false;
    }

    private void Update()
    {
        float accelMult = 1;
        accelMult = CalculateAccelerationMultiplier();

        float _maxSpeed = maxMoveSpeed;
        float _acceleration = acceleration;
        if (isSprinting) { _maxSpeed = sprint_maxMoveSpeed; _acceleration = sprint_acceleration; }
        _maxSpeed *= moveInputStrength;


        // if player is currently above max speed, they can only move AGAINST their current move direction
        // if they are not doing that, they will decelerate
        if (Mathf.Abs(xVelocity) > _maxSpeed)
        {
            // player is moving against current velocity
            if (moveDirection != Mathf.Sign(xVelocity))
            {
                // apply movement
                ApplyMovement(accelMult, _maxSpeed, _acceleration);
            }

            // player is moving with current velocity
            else if (moveDirection != 0)
            {
                // decelerate to max speed
                // this is so the player will not go below max speed if they keep trying to move in the current direction
                Decelerate(accelMult);
                if (Mathf.Abs(xVelocity) < _maxSpeed) { xVelocity = _maxSpeed * Mathf.Sign(xVelocity); }
            }

            // no movement
            else if (moveDirection == 0) { Decelerate(accelMult); }
        }
        // player not above max speed, apply movement normally
        else
        {
            ApplyMovement(accelMult, _maxSpeed, _acceleration);

            // cap movement speed
            if (Mathf.Abs(xVelocity) > _maxSpeed)
            {
                xVelocity = _maxSpeed * Mathf.Sign(xVelocity);
            }
        }

        // apply movement value
        rb2d.linearVelocityX = xVelocity;

    }

    /// <summary>
    /// change the player's x velocity based on movment input and movement variable valuess
    /// </summary>
    /// <param name="accelMult"></param>
    /// <param name="_maxSpeed"></param>
    /// <param name="_acceleration"></param>
    private void ApplyMovement(float accelMult, float _maxSpeed, float _acceleration)
    {
        //moving left
        if (moveDirection < 0) { xVelocity -= _acceleration * Time.deltaTime * accelMult; }

        // moving right
        else if (moveDirection > 0) { xVelocity += _acceleration * Time.deltaTime * accelMult; }

        // no imput
        else if (moveDirection == 0)
        {
            // if player is moving, slow them down to gradually bring them to a stop
            if (xVelocity != 0)
            {
                Decelerate(accelMult);
            }
        }
    }

    /// <summary>
    /// returns an acceleration multiplier determined by the player's grounded/midair status
    /// </summary>
    /// <returns></returns>
    private float CalculateAccelerationMultiplier()
    {
        float accelMult = 1;

        if (!groundedScript.isGrounded) { accelMult = midairAccelMult; } // slower movement in air

        return accelMult;
    }

    /// <summary>
    /// slow the x velocity based on deceleration
    /// </summary>
    /// <param name="accelMult"></param>
    private void Decelerate(float accelMult)
    {
        float newXVelocity = Mathf.Abs(xVelocity);
        newXVelocity -= deceleration * Time.deltaTime * accelMult;
        if (newXVelocity < 0) { newXVelocity = 0; }

        xVelocity = newXVelocity * Mathf.Sign(xVelocity);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;

        // stop all movement inputs
        isSprinting = false;
        moveDirection = 0;
    }

}
