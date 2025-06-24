using UnityEngine;

public class CappedFallSpeed : MonoBehaviour
{
    [SerializeField] float fallSpeedCap = 14;
    [SerializeField] Rigidbody2D rb2d;

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb2d.linearVelocityY) > fallSpeedCap)
        {
            rb2d.linearVelocityY = fallSpeedCap * Mathf.Sign(rb2d.linearVelocityY);
        }
    }
}
