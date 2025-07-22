using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public float gameVolume { get; private set; } = 50;
    public float musicVolume { get; private set; } = 50;
    public float sfxVolume { get; private set; } = 50;

    [SerializeField] int windowWidth = 800;
    [SerializeField] int windowHeight = 600;
    public bool isFullscreen { get; private set; }

    [Space]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float volumeMax = 100;



    public static Settings Instance;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }

        // initialize volume values if they have been changed in the past
        if (PlayerPrefs.HasKey("GameVolume")) { gameVolume = PlayerPrefs.GetFloat("GameVolume"); }
        if (PlayerPrefs.HasKey("MusicVolume")) { musicVolume = PlayerPrefs.GetFloat("MusicVolume"); }
        if (PlayerPrefs.HasKey("SFXVolume")) { sfxVolume = PlayerPrefs.GetFloat("SFXVolume"); }


        // fetch pre-existing window size settings (if they exist)
        if (PlayerPrefs.HasKey("WindowWidth") && PlayerPrefs.HasKey("WindowHeight")) 
        {
            windowWidth = PlayerPrefs.GetInt("WindowWidth");
            windowHeight = PlayerPrefs.GetInt("WindowHeight");
        }

        // apply window size if in windowed mode
        if (Screen.fullScreenMode == FullScreenMode.Windowed) { Screen.SetResolution(windowWidth, windowHeight, false); }

        if (Screen.fullScreenMode == FullScreenMode.Windowed) { isFullscreen = false; }
        else { isFullscreen = true; }

    }

    private void Start()
    {
        // apply volume values to the mixer (doesnt work in awake, that's why its in start)
        UpdateMixerVolumes();
    }

    public void ChangeGameVolume(float newVolume)
    {
        gameVolume = newVolume;
        SaveVolumeValues();
    }

    public void RaiseGameVolBy5() { ChangeGameVolume(CapVolume(gameVolume + 5)); }
    public void LowerGameVolBy5() { ChangeGameVolume(CapVolume(gameVolume - 5)); }


    public void ChangeMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        SaveVolumeValues();
    }

    public void RaiseMusicVolBy5() { ChangeMusicVolume(CapVolume(musicVolume + 5)); }
    public void LowerMusicVolBy5() { ChangeMusicVolume(CapVolume(musicVolume - 5)); }


    public void ChangeSFXVolume(float newVolume)
    {
        sfxVolume = newVolume;
        SaveVolumeValues();
    }

    public void RaiseSFXVolBy5() { ChangeSFXVolume(CapVolume(sfxVolume + 5)); }
    public void LowerSFXVolBy5() { ChangeSFXVolume(CapVolume(sfxVolume - 5)); }


    /// <summary>
    /// restricts a float to the volume minimums and maximums (0 and 100).
    /// returns a float that is capped within those values
    /// </summary>
    /// <param name="newVolume"></param>
    /// <returns></returns>
    float CapVolume(float newVolume)
    {
        // below min
        if (newVolume < 0) { newVolume = 0; }
        // above max
        else if (newVolume > volumeMax) { newVolume = volumeMax; }

        return newVolume;
    }

    /// <summary>
    /// saves all volume values to playerprefs
    /// </summary>
    private void SaveVolumeValues()
    {
        PlayerPrefs.SetFloat("GameVolume", gameVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        UpdateMixerVolumes();
    }

    /// <summary>
    /// updates the volumes in the audio mixer to match the values from settings
    /// </summary>
    private void UpdateMixerVolumes()
    {
        // volume values cant be zero otherwise the calculation returns infinity and it messes up the volume values
        if (gameVolume == 0) { gameVolume = 0.0001f; }
        if (musicVolume == 0) { musicVolume = 0.0001f; }
        if (sfxVolume == 0) { sfxVolume = 0.0001f; }

        audioMixer.SetFloat("GameVolume", Mathf.Log10(gameVolume/100) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume/100) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume / 100) * 20);

        // set volume values back to zero (if they were adjusted) for display purposes
        if (gameVolume == 0.0001f) { gameVolume = 0; }
        if (musicVolume == 0.0001f) { musicVolume = 0; }
        if (sfxVolume == 0.0001f) {  sfxVolume = 0; }
    }

    public void SetFullscreen(bool fullscreen)
    {
        if (fullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow; 
        }
        else { Screen.SetResolution(windowWidth, windowHeight, false); }

        isFullscreen = fullscreen;
    }

    public void LowerWindowSize()
    {
        // do nothing in fullscreen
        if (Screen.fullScreenMode != FullScreenMode.Windowed) { return; }

        if (Screen.width == 800) { windowWidth = 640; windowHeight = 480; }
        else if (Screen.width == 1024) { windowWidth = 800; windowHeight = 600; }
        else if (Screen.width != 640) { windowWidth = 800; windowHeight = 600; } // reset to defaults as fallback

        Screen.SetResolution(windowWidth, windowHeight, false);
        PlayerPrefs.SetInt("WindowWidth", windowWidth);
        PlayerPrefs.SetInt("WindowHeight", windowHeight);
    }

    public void RaiseWindowSize()
    {
        // do nothing in fullscreen
        if (Screen.fullScreenMode != FullScreenMode.Windowed) { return; }

        if (Screen.width == 640) { windowWidth = 800; windowHeight = 600; }
        else if (Screen.width == 800) { windowWidth = 1024; windowHeight = 768; }
        else if (Screen.width != 1024) { windowWidth = 800; windowHeight = 600; } // reset to defaults as fallback

        Screen.SetResolution(windowWidth, windowHeight, false);
        PlayerPrefs.SetInt("WindowWidth", windowWidth);
        PlayerPrefs.SetInt("WindowHeight", windowHeight);
    }
}
