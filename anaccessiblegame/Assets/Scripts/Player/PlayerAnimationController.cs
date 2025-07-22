using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Grounded grounded;
    [SerializeField] Movement movement;
    [SerializeField] Animator animator;

    private void Update()
    {
        // flip sprite to face move direction
        if (rb2d.linearVelocityX > 0.1f) { FaceSprite(1); }
        else if (rb2d.linearVelocityX < -0.1f) { FaceSprite(-1); }

        animator.speed = 1;

        if (grounded.isGrounded && rb2d.linearVelocityX == 0) { animator.Play("Idle"); }

        else if (grounded.isGrounded && rb2d.linearVelocityX != 0) 
        { 
            animator.Play("Walk");

            // animation speed depends on how fast player is moving
            animator.speed = Mathf.Abs(rb2d.linearVelocityX) / movement.maxMoveSpeed;
        }

        else if (!grounded.isGrounded) { animator.Play("Jump_Static"); }
    }

    /// <summary>
    /// changes the scale of the players sprite so it faces in the given direction
    /// </summary>
    /// <param name="direction"></param>
    public void FaceSprite(int direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (direction < 0) { transform.localScale = new Vector2(-1, transform.localScale.y); }
    }

}
