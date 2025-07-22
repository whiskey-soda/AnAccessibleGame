using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnscreenInput : MonoBehaviour
{
    EventSystem eventSystem;

    [SerializeField] Selectable defaultUIElement;
    Selectable selectedUIElement;

    [SerializeField] PauseMenu pauseMenu;

    [Space]

    [SerializeField] AudioClip menuHover;
    [SerializeField] AudioClip menuClick;

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
        if (!pauseMenu.isOpen || element == null) { return; }

        eventSystem.SetSelectedGameObject(element.gameObject);
        selectedUIElement = element;

        // sound is not played in this method because this method is used to re-select the appropriate button
        // if it gets un-selected, such as when the player clicks ANYTHING

    }

    public void Up() 
    {
        if (!pauseMenu.isOpen) { return; } 
        SelectElement(selectedUIElement.navigation.selectOnUp);

        // play hover sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuHover); }
    }
    public void Down() 
    {
        if (!pauseMenu.isOpen) { return; } 
        SelectElement(selectedUIElement.navigation.selectOnDown);

        // play hover sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuHover); }
    }
    public void Left() 
    { 
        if (!pauseMenu.isOpen) { return; } 
        SelectElement(selectedUIElement.navigation.selectOnLeft);

        // play hover sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuHover); }
    }
    public void Right() 
    { 
        if (!pauseMenu.isOpen) { return; } 
        SelectElement(selectedUIElement.navigation.selectOnRight);

        // play hover sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuHover); }
    }

    public void Select()
    {
        if (!pauseMenu.isOpen) { return; }

        if (selectedUIElement is Button) 
        {
            ((Button)selectedUIElement).onClick.Invoke();

            // play click sound
            if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuClick); }
        }
        else if (selectedUIElement is Toggle) 
        {
            // flip toggle
            { ((Toggle)selectedUIElement).isOn = !((Toggle)selectedUIElement).isOn; }

            // activate toggle logic
            ((Toggle)selectedUIElement).onValueChanged.Invoke(((Toggle)selectedUIElement).isOn);

            // play click sound
            if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuClick); }
        }
    }

    private void Update()
    {
        // if selected element gets unselected (often due to clicking another ui element), reselect it
        if (pauseMenu.isOpen && selectedUIElement != null && eventSystem.currentSelectedGameObject != selectedUIElement.gameObject)
        {
            SelectElement(selectedUIElement);
        }
    }

}
