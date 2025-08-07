using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] float gameStartDelay = .7f;
    [Space]
    [SerializeField] Animator camAnimator;
    [SerializeField] Image tvStatic;
    [SerializeField] AudioSource tvStaticAudioSource;
    [SerializeField] AnimationClip panToGameplay;
    [Space]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] PlayerControl playerControl;
    [SerializeField] SpawnController spawnController;
    [SerializeField] SoundController soundController;
    [SerializeField] Animator controllerPosAnimator;
    Accessibility accessibility;
    [SerializeField] Transform endingCutsceneParent;
    [SerializeField] GameObject endingCutscenePrefab;

    public static SceneController Instance;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    private void Start()
    {
        accessibility = Accessibility.Instance;
    }

    public void TransitionToGameplay()
    {
        camAnimator.Play("PanToGameplay");
        Invoke(nameof(StartGame), gameStartDelay + panToGameplay.length); // start game after pan is done + a delay
    }

    public void GoToMainMenu()
    {
        soundController.StopAllSounds(); // prevents game from finishing a sound effect while static starts
        SetTvStatic(true);
        camAnimator.Play("PanToMenu");

        pauseMenu.ClosePauseMenu();
        pauseMenu.DisablePausing();
        playerControl.DisableControl();
        soundController.SetSoundEffectsEnabled(false);
        soundController.SetMusicEnabled(false);
        controllerPosAnimator.Play("ControllerSlideOut");

    }

    void StartGame()
    {
        InitializePlatformer();

        // slight delay to prevent the player seeing any loading setup
        Invoke(nameof(ShowGame), .1f);
    }

    void InitializePlatformer()
    {
        pauseMenu.EnablePausing();
        playerControl.EnableControl();
        spawnController.Spawn();
        soundController.SetSoundEffectsEnabled(true);
        soundController.SetMusicEnabled(true);

        // disable all accessibility options on startup
        if (accessibility != null)
        {
            accessibility.SetTimestop(false);
            accessibility.SetJumpToggle(false);
            accessibility.SetRunToggle(false);
        }

        // create an ending cutscene if none exists
        if (FindAnyObjectByType<Ending>() == null)
        {
            Instantiate(endingCutscenePrefab, endingCutsceneParent);
        }
    }

    void SetTvStatic(bool isOn)
    {
        // set image color alpha
        Color newColor = tvStatic.color;
        if (isOn) { newColor.a = 1; }
        else { newColor.a = 0; }

        tvStatic.color = newColor;

        // set audiosource
        tvStaticAudioSource.mute = !isOn;
        if (isOn) { tvStaticAudioSource.Play(); }
    }

    void ShowGame()
    {
        SetTvStatic(false);
        controllerPosAnimator.Play("ControllerSlideIn");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

}
