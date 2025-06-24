using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowSizeChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionDisplay;
    [SerializeField] Button lowerSizeButton;
    [SerializeField] Button raiseSizeButton;

    // Update is called once per frame
    void Update()
    {
        // only update res display in windowed mode
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            resolutionDisplay.text = $"{Screen.width} x {Screen.height}";
        }

        // disable window size buttons in fullscreen
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            lowerSizeButton.interactable = true;
            raiseSizeButton.interactable = true;
        }
        else
        {
            lowerSizeButton.interactable = false;
            raiseSizeButton.interactable = false;
        }
    }
}
