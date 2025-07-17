using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    
    Settings settings;

    private void Start()
    {
        settings = Settings.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        // if toggle does not match setting, make them match
        if (toggle.isOn != settings.isFullscreen) { toggle.isOn = settings.isFullscreen; }
    }
}
