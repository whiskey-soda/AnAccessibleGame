using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowSizeChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionDisplay;

    // Update is called once per frame
    void Update()
    {
        resolutionDisplay.text = $"{Screen.width} x {Screen.height}";

    }
}
