using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    enum VolumeSetting { game, music, sfx }

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI volumeNumberTMP;
    [Space]
    [SerializeField] VolumeSetting volumeSetting;

    void Start()
    {
        InitializeVolumeSettings();

    }

    private void InitializeVolumeSettings()
    {
        switch (volumeSetting)
        {
            case VolumeSetting.game:
                slider.value = Settings.Instance.gameVolume;
                break;

            case VolumeSetting.music:
                slider.value = Settings.Instance.musicVolume;
                break;

            case VolumeSetting.sfx:
                slider.value = Settings.Instance.sfxVolume;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        volumeNumberTMP.text = ((int)slider.value).ToString();
    }
}
