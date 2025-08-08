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
    /// <summary>
    /// disables movement and jumping, and cancels any ongoing jump or sprint actions
    /// </summary>
    public void DisableControl()
    {
        movement.DisableMovement();
        jump.DisableJump();
    }

    /// <summary>
    /// disables movement and jumping. can cancel ongoing jump/sprint actions depending on parameter
    /// </summary>
    /// <param name="retainControlStatuses"></param>
    public void DisableControl(bool retainControlStatuses)
    {
        if (retainControlStatuses)
        {
            // methods that do not cancel ongoing jumps/sprinting
            movement.DisableMovement(true);
            jump.DisableJump(true);
        }
        // cancels ongoing jumps and sprinting
        else { DisableControl(); }
    }
}
