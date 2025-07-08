using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnscreenInput : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] Selectable defaultUIElement;
    Selectable selectedUIElement;

    bool menuOpen = false;

    public static OnscreenInput Instance;
    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }

        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    /// <summary>
    /// toggles menu open and closed.
    /// if menu is opening, selects the default button
    /// </summary>
    public void ToggleMenu()
    {
        // close the open menu
        if (menuOpen) { menuOpen = false; }
        else // open the menu
        {
            menuOpen = true;
            SelectElement(defaultUIElement);
        }
    }

    public void SelectElement(Selectable element)
    {
        // do nothing if menu is not open or button is invalid
        if (!menuOpen || element == null) { return; }

        eventSystem.SetSelectedGameObject(element.gameObject);
        selectedUIElement = element;
    }

    public void Up() { if (!menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnUp); }
    public void Down() { if (!menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnDown); }
    public void Left() { if (!menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnLeft); }
    public void Right() { if (!menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnRight); }

    public void Select()
    {
        if (!menuOpen) { return; }

        if (selectedUIElement is Button) { ((Button)selectedUIElement).onClick.Invoke(); }
        else if (selectedUIElement is Toggle) 
        {
            // flip toggle
            { ((Toggle)selectedUIElement).isOn = !((Toggle)selectedUIElement).isOn; }

            // activate toggle logic
            ((Toggle)selectedUIElement).onValueChanged.Invoke(((Toggle)selectedUIElement).isOn); 
        }
    }


}
