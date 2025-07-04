using UnityEngine;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    List<GameObject> menuPanels;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject accessibilityMenu;
    [SerializeField] GameObject settingsMenu;

    void Pause()
    {
        Time.timeScale = 0;
    }

    void Resume()
    {
        if (Accessibility.Instance != null) { Time.timeScale = (float)Accessibility.Instance.gameSpeedPercent / 100; }
        else { Time.timeScale = 1; }
    }

    public void ShowPauseMenu()
    {
        Pause();
        ShowMenu(pauseMenu);
    }
    public void ClosePauseMenu()
    {
        HideAllMenus();
        Resume();
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

}
