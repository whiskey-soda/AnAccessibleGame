using TMPro;
using UnityEngine;

public class VolumeDisplays : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameVolText;
    [SerializeField] TextMeshProUGUI musicVolText;
    [SerializeField] TextMeshProUGUI sfxVolText;

    // Update is called once per frame
    void Update()
    {
        if (gameVolText != null && gameVolText.isActiveAndEnabled) { gameVolText.text = Settings.Instance.gameVolume.ToString(); }

        if (musicVolText != null && musicVolText.isActiveAndEnabled) { musicVolText.text = Settings.Instance.musicVolume.ToString(); }

        if (sfxVolText != null && sfxVolText.isActiveAndEnabled) { sfxVolText.text = Settings.Instance.sfxVolume.ToString(); }
    }
}
