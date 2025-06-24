using UnityEngine;

public class Accessibility : MonoBehaviour
{
    public int GameSpeedPercent { get; private set; } = 100;

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
        Time.timeScale = GameSpeedPercent/100;
    }

    public void RaiseGameSpeed()
    {
        if (GameSpeedPercent >= 100) { return; }

        GameSpeedPercent += 10;
    }

    public void LowerGameSpeed()
    {
        if (GameSpeedPercent <= 0) { return; }

        GameSpeedPercent -= 10;
    }
}
