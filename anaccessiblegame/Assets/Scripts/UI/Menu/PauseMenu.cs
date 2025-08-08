using UnityEngine;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject accessibilityMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] GameObject screenDim;

    [Space]
    [SerializeField] AudioClip menuOpen;
    public bool isOpen { get; private set; } = false;

    public bool canPause { get; private set; } = true;

    [Space]
    // prevents bug where in-menu inputs would buffer for when the game unpaused
    [SerializeField] PlayerControl playerControl;

    void Pause()
    {
        // do nothing if pausing is disabled
        if (!canPause) { return; } 

        Time.timeScale = 0;
    }

    void Resume()
    {
        if (Accessibility.Instance != null) 
        {
            // only resume time IF timestop is off.
            // resuming time with timestop on keeps time at full speed until a move input is pressed
            if (!Accessibility.Instance.timeStop)
            {
                Time.timeScale = (float)Accessibility.Instance.gameSpeedPercent / 100;
            }
        }
        else { Time.timeScale = 1; }
    }

    public void ShowPauseMenu()
    {

        Pause();
        ShowMenu(pauseMenu);

        screenDim.SetActive(true);

        isOpen = true;

        playerControl.DisableControl(true);


        // play menu opening sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuOpen); }
    }
    public void ClosePauseMenu()
    {
        HideAllMenus();
        screenDim.SetActive(false);

        Resume();

        isOpen = false;

        playerControl.EnableControl();

        // play menu opening sound
        if (SoundController.Instance != null) { SoundController.Instance.PlaySoundEffect(menuOpen); }
    }

    public void ShowAccessibilitySubmenu()
    {
        ShowMenu(accessibilityMenu);
    }

    public void ShowSettingsSubmenu()
    {
        ShowMenu(settingsMenu);
    }

    void ShowMenu(GameObject menuObject)
    {
        HideAllMenus();
        menuObject.SetActive(true);
    }

    void HideAllMenus()
    {
        pauseMenu.SetActive(false);
        accessibilityMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        // do nothing if pausing is disabled
        if (!canPause) { return; }

        if (isOpen)
        {
            ClosePauseMenu();
        }
        else { ShowPauseMenu();}
    }

    public void EnablePausing() { canPause = true; }

    public void DisablePausing() { canPause = false; }

}
