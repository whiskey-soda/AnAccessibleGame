using UnityEngine;

public class SpawnController : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] PlayerControl playerControl;
    [SerializeField] Transform initialSpawnTransform;

    Vector2 currentSpawn;

    public static SpawnController Instance;

    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }



        SetSpawn(initialSpawnTransform.position);
    }

    public void SetSpawn(Vector2 newSpawnPos)
    {
        currentSpawn = newSpawnPos;
    }

    /// <summary>
    /// respawns player at a specified position
    /// </summary>
    /// <param name="spawnPos"></param>
    public void Respawn(Vector2 spawnPos)
    {
        Debug.Log("respawning...");
        player.transform.position = spawnPos;
        playerControl.EnableControl();
    }

    /// <summary>
    /// overload of respawn method that respawns player at current registered spawn point
    /// </summary>
    public void Respawn()
    {
        Respawn(currentSpawn);
    }

    public void Spawn()
    {
        SetSpawn(initialSpawnTransform.position);
        Respawn(currentSpawn);
    }
}
