using UnityEngine;
using UnityEngine.UI;

public class ToggleRunToggle : MonoBehaviour
{
    Accessibility accessibility;
    [SerializeField] Toggle toggle;

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // if toggle does not match setting, make them match
        if (toggle.isOn != accessibility.toggleRun) { toggle.isOn = accessibility.toggleRun; }

        // cannot change "toggle" type settings while timestop setting is on
        // timestop automatically activates toggle jump and toggle run
        if (accessibility.timeStop) { toggle.interactable = false; }
        else { toggle.interactable = true; }
    }
}
