using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private void Awake()
    {

        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
