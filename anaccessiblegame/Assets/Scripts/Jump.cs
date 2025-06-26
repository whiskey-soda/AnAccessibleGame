using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] Grounded grounded;
    [SerializeField] Movement movement;

    [SerializeField] float jumpHeight = 5;
    [SerializeField] float sprintingJumpHeight = 6;
    float maxJumpHeight = 0;
    float heightJumped = 0;

    [Space]
    [SerializeField] float jumpSpeed = 10;

    bool isJumping = false;

    Accessibility accessibility;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    private void Update()
    {
        // reduce jump height if player stops sprinting and slows down
        if (!movement.isSprinting || Mathf.Abs(movement.xVelocity) < movement.sprint_maxMoveSpeed)
        {
            maxJumpHeight = jumpHeight;
        }

        if (isJumping && heightJumped < maxJumpHeight)
        {
            rb2d.linearVelocityY = jumpSpeed;
        }
        else
        {
            StopJump(); 
        }

        heightJumped += jumpSpeed * Time.deltaTime;
    }

    public void DoJump(bool inputPressed)
    {
        // toggle jump setting on
        // jump starts on click, stops on click if player is currently jumping
        // checks for inputPressed to ensure nothing happens on button release
        if (accessibility.toggleJump && inputPressed == true)
        {
            if (isJumping) { StopJump(); }
            else { TryStartJump(); }
        }
        // default logic
        // jump on click, stop jumping on release
        else if (!accessibility.toggleJump)
        {
            if (inputPressed) { TryStartJump(); }
            else { StopJump(); }
        }
    }

    private void TryStartJump()
    {
        // start jump if player is on ground
        if (grounded.isGrounded)
        {
            // set max jump height higher if player is sprinting at max speed
            if (movement.isSprinting && Mathf.Abs(movement.xVelocity) >= movement.sprint_maxMoveSpeed)
            {
                maxJumpHeight = sprintingJumpHeight;
            }
            else
            {
                maxJumpHeight = jumpHeight;
            }

            isJumping = true;
            heightJumped = 0;
        }
    }

    public void StopJump()
    {
        isJumping= false;
    }

}
