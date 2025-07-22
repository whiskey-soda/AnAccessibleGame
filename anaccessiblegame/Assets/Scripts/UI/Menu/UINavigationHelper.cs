using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavigationHelper : MonoBehaviour
{
    EventSystem eventSystem;
    [SerializeField] Selectable elementToSelect;

    private void Awake()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    public void SelectElement()
    {
        eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }

    public void SelectOnscreenElement()
    {
        OnscreenInput.Instance.SelectElement(elementToSelect);
    }
}
