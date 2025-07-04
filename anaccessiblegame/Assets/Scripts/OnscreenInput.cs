using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnscreenInput : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] Button defaultButton;
    Button selectedButton;

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
            SelectButton(defaultButton);
        }
    }

    public void SelectButton(Selectable button)
    {
        // do nothing if menu is not open or button is invalid
        if (!menuOpen || button == null) { return; }

        eventSystem.SetSelectedGameObject(button.gameObject);
        selectedButton = button.GetComponent<Button>();
    }

    public void Up() { SelectButton(selectedButton.navigation.selectOnUp); }
    public void Down() { SelectButton(selectedButton.navigation.selectOnDown); }
    public void Left() { SelectButton(selectedButton.navigation.selectOnLeft); }
    public void Right() { SelectButton(selectedButton.navigation.selectOnRight); }

    public void Select()
    {
        if (menuOpen) { selectedButton.onClick.Invoke(); }
    }

    // test
    private void Update()
    {
        Debug.Log(eventSystem.currentSelectedGameObject.name);
    }

}
