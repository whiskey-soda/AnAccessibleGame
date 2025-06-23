using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] public bool isGrounded { get; private set; }
    [SerializeField] public bool onHead { get; private set; }

    [Header("Edit Grounded")]
    [SerializeField] Collider2D feet;
    [SerializeField] float detectionLength;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Player;

    //Jump jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        CheckOnHead();
    }
    #region Grounded

    public void CheckGrounded()
    {

        Vector2 castOrigin = new Vector2(feet.bounds.center.x, feet.bounds.min.y); //Defines the center and middle of the collider
        Vector2 castSize = new Vector2(feet.bounds.size.x, detectionLength); //Defines the width as the collider and height customizable


        LayerMask mask = Ground; //Set so that we can add other layers
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0f, Vector2.down, detectionLength, mask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            //jump.coyoteTimer = jump.coyoteTime; // reset on ground
        }
        else
        {
            //jump.coyoteTimer -= Time.deltaTime;
        }
    }

    public void CheckOnHead()
    {
        Vector2 castOrigin = new Vector2(feet.bounds.center.x, feet.bounds.min.y); //Defines the center and middle of the collider
        Vector2 castSize = new Vector2(feet.bounds.size.x, detectionLength); //Defines the width as the collider and height customizable


        LayerMask mask = Player; //Set so that we can add other layers
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0f, Vector2.down, detectionLength, mask);

        if (hit.collider != null)
        {
            onHead = true;
        }
        else
        {
            onHead = false;
        }

        Debug.Log("On Head: " + onHead);
    }

    #endregion
}
