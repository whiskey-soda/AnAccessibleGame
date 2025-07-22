using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameSpeedDisplay;
    [SerializeField] Button lowerSpeedButton;
    [SerializeField] Button raiseSpeedButton;

    Accessibility accessibility;

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    private void Update()
    {
        gameSpeedDisplay.text = accessibility.gameSpeedPercent.ToString() + "%";

        // removed because there is no indication that a ui element is "disabled" or non-interactable
        /*
        // disable lowering game speed if it is at 10% or lower
        if (accessibility.gameSpeedPercent <= 10) { lowerSpeedButton.interactable = false; }
        else { lowerSpeedButton.interactable = true; }

        // disable raising game speed if it is at 100% or higher
        if (accessibility.gameSpeedPercent >= 100) { raiseSpeedButton.interactable = false; }
        else { raiseSpeedButton.interactable = true; }
        */
    }

}
