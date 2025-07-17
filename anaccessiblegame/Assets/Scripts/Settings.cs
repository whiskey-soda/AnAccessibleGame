using UnityEngine;

public class Settings : MonoBehaviour
{
    public float gameVolume { get; private set; } = 50;
    public float musicVolume { get; private set; } = 50;
    public float sfxVolume { get; private set; } = 50;

    [SerializeField] int windowWidth = 800;
    [SerializeField] int windowHeight = 600;
    public bool isFullscreen { get; private set; }

    [Space]
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
