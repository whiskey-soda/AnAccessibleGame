using UnityEngine;

public class EasyMovement : MonoBehaviour
{
    [SerializeField] bool active = false;

    [SerializeField] Movement movement;
    [SerializeField] Jump jump;

    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }

        if (Input.GetKeyDown(KeyCode.A)) { movement.Move(-1); }
        if (Input.GetKeyDown(KeyCode.D)) {  movement.Move(1); }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) { movement.Move(0); }

        if (Input.GetKeyDown(KeyCode.LeftShift)) { movement.Sprint(true); }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { movement.Sprint(false); }

        if (Input.GetKeyDown(KeyCode.Space)) { jump.DoJump(true); }
        if (Input.GetKeyUp(KeyCode.Space)) { jump.DoJump(false); }
    }
}
