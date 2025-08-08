using UnityEngine;
using UnityEngine.Events;

public class Accessibility : MonoBehaviour
{
    public int gameSpeedPercent { get; private set; } = 100;

    public bool toggleRun = false;
    public bool toggleJump = false;
    public bool timeStop = false;


    public UnityEvent toggleRunDisabled;
    public UnityEvent toggleJumpDisabled;

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
        if (gameSpeedPercent <= 10) { return; }

        gameSpeedPercent -= 10;
    }

    public void SetGameSpeed(int newGameSpeedPercent)
    {
        gameSpeedPercent = newGameSpeedPercent;
    }

    public void SetRunToggle(bool isOn)
    {
        // do not turn off if timestop is on
        if (!isOn && timeStop) { return; }

        toggleRun = isOn;
    }

    public void SetJumpToggle(bool isOn)
    {
        // do not turn off if timestop is on
        if (!isOn && timeStop) { return; }

        toggleJump = isOn;
    }

    public void SetTimestop(bool isOn)
    {
        timeStop = isOn;

        if (timeStop)
        {
            SetRunToggle(isOn);
            SetJumpToggle(isOn);
        }
    }

}
