using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnscreenInput : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] Selectable defaultUIElement;
    Selectable selectedUIElement;

    [SerializeField] PauseMenu pauseMenu;

    public static OnscreenInput Instance;
    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }

        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    /// <summary>
    /// selects the default button in the menu if opening the pause menu is enabled
    /// </summary>
    public void SelectDefaultElement()
    {
        if (pauseMenu.canPause)
        {
            SelectElement(defaultUIElement);
        }
    }

    public void SelectElement(Selectable element)
    {
        // do nothing if menu is not open or button is invalid
        if (!pauseMenu.menuOpen || element == null) { return; }

        eventSystem.SetSelectedGameObject(element.gameObject);
        selectedUIElement = element;
    }

    public void Up() { if (!pauseMenu.menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnUp); }
    public void Down() { if (!pauseMenu.menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnDown); }
    public void Left() { if (!pauseMenu.menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnLeft); }
    public void Right() { if (!pauseMenu.menuOpen) { return; } SelectElement(selectedUIElement.navigation.selectOnRight); }

    public void Select()
    {
        if (!pauseMenu.menuOpen) { return; }

        if (selectedUIElement is Button) { ((Button)selectedUIElement).onClick.Invoke(); }
        else if (selectedUIElement is Toggle) 
        {
            // flip toggle
            { ((Toggle)selectedUIElement).isOn = !((Toggle)selectedUIElement).isOn; }

            // activate toggle logic
            ((Toggle)selectedUIElement).onValueChanged.Invoke(((Toggle)selectedUIElement).isOn); 
        }
    }

    private void Update()
    {
        // if selected element gets unselected (often due to clicking another ui element), reselect it
        if (pauseMenu.menuOpen && selectedUIElement != null && eventSystem.currentSelectedGameObject != selectedUIElement.gameObject)
        {
            SelectElement(selectedUIElement);
        }
    }

}
