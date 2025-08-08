using UnityEngine;
using UnityEngine.UI;

public class TimingAssistToggle : MonoBehaviour
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
        if (toggle.isOn != accessibility.timeStop) { toggle.isOn = accessibility.timeStop; }
    }
}
