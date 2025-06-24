using TMPro;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonTMP;

    // Update is called once per frame
    void Update()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed) { buttonTMP.text = "windowed"; }
        else { buttonTMP.text = "fullscreen"; }
    }
}
