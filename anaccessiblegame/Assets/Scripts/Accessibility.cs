using UnityEngine;
using UnityEngine.Events;

public class Accessibility : MonoBehaviour
{
    public int gameSpeedPercent { get; private set; } = 100;

    public bool toggleRun = false;
    public bool toggleJump = false;


    public UnityEvent toggleRunDisabled;

    public static Accessibility Instance;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = gameSpeedPercent/100;
    }

    public void RaiseGameSpeed()
    {
        if (gameSpeedPercent >= 100) { return; }

        gameSpeedPercent += 10;
    }

    public void LowerGameSpeed()
    {
        if (gameSpeedPercent <= 0) { return; }

        gameSpeedPercent -= 10;
    }

    public void ToggleRunToggle()
    {
        if (toggleRun) { toggleRun = false; }
        else { toggleRun = true; }
    }

    public void ToggleJumpToggle()
    {
        if (toggleJump) { toggleJump = false; }
        else { toggleJump = true; }
    }

}
