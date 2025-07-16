using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;


    // Update is called once per frame
    void Update()
    {
        // if toggle does not match setting, make them match
        if (toggle.isOn != (Screen.fullScreenMode == FullScreenMode.FullScreenWindow) ) { toggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow; ; }
    }
}
