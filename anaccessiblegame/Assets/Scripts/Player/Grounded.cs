using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    [SerializeField] AudioClip landingSound;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            // not grounded, therefore player is landing after being in the air
            if (!isGrounded)
            {
                // play jump sound
                if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(landingSound); }
            }

            isGrounded = true;

        }
    }

}
