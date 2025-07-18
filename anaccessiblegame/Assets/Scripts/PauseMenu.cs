using UnityEngine;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject accessibilityMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] GameObject screenDim;

    public bool menuOpen { get; private set; } = false;

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

        menuOpen = true;

        playerControl.DisableControl();
    }
    public void ClosePauseMenu()
    {
        HideAllMenus();
        screenDim.SetActive(false);

        Resume();

        menuOpen = false;

        playerControl.EnableControl();
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

        if (menuOpen)
        {
            ClosePauseMenu();
        }
        else { ShowPauseMenu();}
    }

    public void EnablePausing() { canPause = true; }

    public void DisablePausing() { canPause = false; }

}
