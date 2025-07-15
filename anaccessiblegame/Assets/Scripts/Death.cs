using UnityEngine;

[RequireComponent(typeof(PlayerControl))]

// to be placed on the player object
public class Death : MonoBehaviour
{
    [SerializeField] Transform deathHeight;
    [SerializeField] float respawnDelay = 2;

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
        }
    }

    void Respawn()
    {
        if (SpawnController.Instance != null) { SpawnController.Instance.Respawn(); }
        isDead= false;
    }
}
