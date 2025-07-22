using UnityEngine;

[RequireComponent(typeof(PlayerControl))]

// to be placed on the player object
public class Death : MonoBehaviour
{
    [SerializeField] Transform deathHeight;
    [SerializeField] float respawnDelay = 2;

    [SerializeField] PauseMenu pauseMenu;

    PlayerControl playerControl;

    bool isDead = false;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deathHeight.position.y && !isDead)
        {
            // disable control, then respawn after a delay
            playerControl.DisableControl();
            if (SpawnController.Instance != null) { Invoke(nameof(Respawn), respawnDelay); }

            isDead = true;

            // game should run at full speed if player dies to prevent timing issues
            Time.timeScale = 1;

            // disable pausing during death
            pauseMenu.DisablePausing();
        }
    }

    void Respawn()
    {
        if (SpawnController.Instance != null) { SpawnController.Instance.Respawn(); }
        isDead= false;

        // sets game speed to 0 if timestop is on (to prevent letting game run at full speed in timestop)
        if (Accessibility.Instance != null)
        {
            if (Accessibility.Instance.timeStop)
            {
                Time.timeScale = 0;
            }
            else
            {
                // game resumes at normal speed if timestop is off
                Time.timeScale = Accessibility.Instance.gameSpeedPercent / 100;
            }
        }

        // re-enable pausing once player has respawned
        pauseMenu.EnablePausing();
    }
}
