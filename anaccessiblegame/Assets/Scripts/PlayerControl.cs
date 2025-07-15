using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Jump jump;


    public void EnableControl()
    {
        movement.EnableMovement();
        jump.EnableJump();
    }

    public void DisableControl()
    {
        movement.DisableMovement();
        jump.DisableJump();
    }
}
