using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    enum VolumeSetting { game, music, sfx, commentary }

    [SerializeField] Slider slider;
    [SerializeField] float sliderSteps = 20;
    [Space]
    [SerializeField] VolumeSetting volumeSetting;

    Settings settings;

    private void Awake()
    {
        sliderSteps = slider.maxValue;
    }

    void Start()
    {
        settings = Settings.Instance;
        SyncVolumeSettings();

    }

    private void SyncVolumeSettings()
    {
        if (settings == null) { return; }


        switch (volumeSetting)
        {
            case VolumeSetting.game:
                slider.value = Settings.Instance.gameVolume / (settings.volumeMax / sliderSteps);
                break;

            case VolumeSetting.music:
                slider.value = Settings.Instance.musicVolume / (settings.volumeMax / sliderSteps);
                break;

            case VolumeSetting.sfx:
                slider.value = Settings.Instance.sfxVolume / (settings.volumeMax / sliderSteps);
                break;

            case VolumeSetting.commentary:
                slider.value = Settings.Instance.commentaryVolume / (settings.volumeMax / sliderSteps);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SyncVolumeSettings();
    }

    public void ChangeVolumeLevelWithSlider(float newSliderValue)
    {
        if (settings == null) { return; }

        switch (volumeSetting)
        {
            case VolumeSetting.game:
                settings.ChangeGameVolume(slider.value * (settings.volumeMax / sliderSteps));
                break;

            case VolumeSetting.music:
                settings.ChangeMusicVolume(slider.value * (settings.volumeMax / sliderSteps));
                break;

            case VolumeSetting.sfx:
                settings.ChangeSFXVolume(slider.value * (settings.volumeMax / sliderSteps));
                break;

            case VolumeSetting.commentary:
                settings.ChangeCommentaryVolume(slider.value * (settings.volumeMax / sliderSteps));
                break;
        }
    }
}
