using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] public bool isGrounded { get; private set; }

    [SerializeField] LayerMask Ground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false;
        }
    }

}
