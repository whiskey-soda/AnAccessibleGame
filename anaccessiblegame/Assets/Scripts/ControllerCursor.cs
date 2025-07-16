using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(VirtualMouseInput))]
public class ControllerCursor : MonoBehaviour
{
    RectTransform rect;
    VirtualMouseInput virtualMouseInput;

    [SerializeField] Vector2 defaultPosition = new Vector2(400, 300);
    [SerializeField] Image cursorImage;
    [Space]
    [SerializeField] float deadzone = .35f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // show mouse cursor if there is mouse movement
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0)
        {
            Cursor.visible = true;
            cursorImage.enabled = false;
        }
        // hide mouse cursor if there is gamepad input
        else if (Gamepad.current != null && (Gamepad.current.leftStick.ReadValue().magnitude > deadzone ||
            Gamepad.current.buttonSouth.wasPressedThisFrame))
        {
            // only initialize controller cursor if it has not yet been initialized
            if (Cursor.visible)
            {
                Cursor.visible = false;

                // move controller cursor to default position (center of screen by default)
                rect.anchoredPosition = defaultPosition;
                InputState.Change(virtualMouseInput.virtualMouse.position, defaultPosition);

                cursorImage.enabled = true;
            }
        }

        // if mouse is in use, move controller cursor to bottom left of the screen so it cant click on anything or block raycasts
        if (Cursor.visible)
        {
            rect.anchoredPosition = Vector2.zero;
            InputState.Change(virtualMouseInput.virtualMouse.position, Vector2.zero);
        }

    }
}
