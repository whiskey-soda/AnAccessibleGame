using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] Grounded grounded;
    [SerializeField] HeadBonk headCollisionDetector;
    [SerializeField] Movement movement;

    [SerializeField] float jumpHeight = 4;
    [SerializeField] float sprintingJumpHeight = 5.5f;
    [SerializeField] float sprintSpeedProportionRequired = .9f;
    float maxJumpHeight = 0;
    float heightJumped = 0;

    [Space]
    [SerializeField] float jumpSpeed = 10;

    [Space]
    [SerializeField] AudioClip jumpSound;

    public bool isJumping { get; private set; } = false;

    bool canJump = true;


    Accessibility accessibility;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        accessibility = Accessibility.Instance;
        accessibility.toggleJumpDisabled.AddListener(StopJump);
    }

    private void Update()
    {
        // reduce jump height if player stops sprinting and slows down
        if (!movement.isSprinting || Mathf.Abs(movement.xVelocity) < (movement.sprint_maxMoveSpeed * sprintSpeedProportionRequired))
        {
            maxJumpHeight = jumpHeight;
        }

        // jump in progress and not complete
        if (isJumping && heightJumped < maxJumpHeight && !headCollisionDetector.headBonking)
        {
            rb2d.linearVelocityY = jumpSpeed;
        }
        // end in-progress jump
        else if (isJumping)
        {
            StopJump(); 
        }

        heightJumped += jumpSpeed * Time.deltaTime;

        // stop jump if player jumping is disabled
        //if (isJumping && !canJump) { StopJump(); }
    }

    public void DoJump(bool inputPressed)
    {
        // prevent jumping if jumping is disabled
        if (!canJump) { return; }

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
            // set max jump height higher if player is sprinting at (near) max speed
            if (movement.isSprinting && Mathf.Abs(movement.xVelocity) >= (movement.sprint_maxMoveSpeed * sprintSpeedProportionRequired) )
            {
                maxJumpHeight = sprintingJumpHeight;
            }
            else
            {
                maxJumpHeight = jumpHeight;
            }

            isJumping = true;
            heightJumped = 0;


            // play jump sound
            if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(jumpSound); }
        }
    }

    public void StopJump()
    {
        isJumping= false;
    }

    public void EnableJump()
    {
        canJump = true;
    }

    /// <summary>
    /// disables jumping and stops any current jumps
    /// </summary>
    public void DisableJump()
    {
        canJump = false;

        if (isJumping) { StopJump(); }
    }

    public void DisableJump(bool retainJumpStatus)
    {
        bool jumpStatus = isJumping;

        DisableJump();

        // re-apply jump status so it does not get canceled
        if (retainJumpStatus) { isJumping = jumpStatus; }
    }

}
