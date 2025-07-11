using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Grounded grounded;
    [SerializeField] Animator animator;

    private void Update()
    {
        if (rb2d.linearVelocityX > 0.1f) { transform.localScale = new Vector2(1, transform.localScale.y); }
        else if (rb2d.linearVelocityX < -0.1f) { transform.localScale = new Vector2(-1, transform.localScale.y); }


        if (grounded.isGrounded && rb2d.linearVelocityX == 0) { animator.Play("Idle"); }

        else if (grounded.isGrounded && rb2d.linearVelocityX != 0) { animator.Play("Walk"); }

        else if (!grounded.isGrounded) { animator.Play("Jump_Static"); }
    }

}
