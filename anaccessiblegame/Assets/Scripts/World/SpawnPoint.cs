using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Transform spawnPositionTransform;

    private void Awake()
    {
        // ensure player detection collider is a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SpawnController.Instance != null) { SpawnController.Instance.SetSpawn(spawnPositionTransform.position); }
        }
    }

}
