using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Collider2D))]

public class Ending : MonoBehaviour
{
    uint endingStage = 0;
    uint completedStage = 0;

    PlayerControl playerControl;
    Movement playerMovement;
    PauseMenu pauseMenu;
    Accessibility accessibility;

    [SerializeField] Transform playerWalkDestination;
    [SerializeField] float walkSpeed = .3f;
    [SerializeField] float playerHesitateDuration = 2;
    [Space]
    [SerializeField] Transform offscreenDestination;
    [SerializeField] NPCController highCultist;
    [Space]
    [SerializeField] float cultistEmergenceDelay = .7f;
    [SerializeField] Transform cultistTentSpawnPoint;
    [SerializeField] GameObject cultistPrefab;
    [SerializeField] uint tentCultistCount = 3;
    [SerializeField] float cultistSpawnsDelay = .4f;
    List<NPCController> spawnedCultists = new List<NPCController>();
    uint cultistsSpawnedCount = 0;
    bool tentCultistsSpawning = false;
    [Space]
    [SerializeField] float winMessageDelay = 1;
    [SerializeField] Animator winTextAnimator;
    [SerializeField] float gameEndDuration = 5;
    SceneController sceneController;

    private void Awake()
    {
        // ensure collider is a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        pauseMenu = FindFirstObjectByType<PauseMenu>();
        accessibility = Accessibility.Instance;

        sceneController = SceneController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement = collision.GetComponent<Movement>();

            CompleteStage(0, 0);
            playerControl.DisableControl();
            pauseMenu.DisablePausing();

            accessibility.SetTimestop(false);
            accessibility.SetJumpToggle(false);
            accessibility.SetRunToggle(false);

        }
    }
    private void Update()
    {
        // run ending at full speed
        if (endingStage > 0) { Time.timeScale = 1; }

        switch(endingStage)
        {
            case 0: // ending not started yet, do nothing
                return;

            // move player towards cultist
            case 1:
                if (playerMovement.transform.position.x < playerWalkDestination.position.x)
                {
                    playerMovement.Move(walkSpeed, true);
                }
                else 
                { 
                    playerMovement.Move(0, true);

                    CompleteStage(1, playerHesitateDuration);
                }

                break;

            // player and high cultist walk offscreen
            case 2:

                // player walks offscreen
                if (playerMovement.transform.position.x < offscreenDestination.position.x)
                {
                    playerMovement.Move(walkSpeed, true);
                }
                else
                {
                    playerMovement.Move(0, true);
                }

                // high cultist walks offscreen
                if (highCultist.transform.position.x < offscreenDestination.position.x)
                {
                    highCultist.Move(walkSpeed, true);
                }
                else
                {
                    highCultist.Move(0, true);
                }

                // high cultist and player offscreen
                // cultists begin emerging from tent after short delay
                if (highCultist.transform.position.x >= offscreenDestination.position.x &&
                    playerMovement.transform.position.x >= offscreenDestination.position.x)
                { CompleteStage(2, cultistEmergenceDelay); }

                break;

            case 3: // cultists emerge from tent and walk offscreen

                // spawn first cultist from the tent
                if (!spawnedCultists.Any() && !tentCultistsSpawning) 
                {
                    cultistsSpawnedCount = 0;
                    SpawnCultistFromTent(); 
                    tentCultistsSpawning = true;                 
                }
                // all cultists already spawned and destroyed
                else if (cultistsSpawnedCount >= tentCultistCount)
                {
                    tentCultistsSpawning = false;
                    CompleteStage(3, winMessageDelay);
                }
                
                break;

            case 4: //show ending text
                if (completedStage <= endingStage) { winTextAnimator.Play("FadeIn"); }
                CompleteStage(4, gameEndDuration);
                break;

            case 5: // return to menu and reset ending
                sceneController.GoToMainMenu();

                // enable commentary
                if (CommentaryController.Instance != null)
                {
                    CommentaryController.Instance.UnlockCommentary();
                }

                // destroy this ending cutscene (will be recreated when platformer is initialized)
                Destroy(gameObject);
                
                break;


        }// end switch

        // logic for controlling spawned cultists
        if (spawnedCultists.Any())
        {
            List<NPCController> cultistsToDespawn = new List<NPCController>(); // helper list for removing offscreen cultists

            foreach (NPCController cultist in spawnedCultists)
            {
                // if cultist is fully faded in, move to the right
                if (cultist.GetComponent<SpriteRenderer>().color.a >= 1)
                {
                    cultist.Move(walkSpeed, true);
                }

                // if cultist is offscreen, remove and despawn it
                if (cultist.transform.position.x >= offscreenDestination.position.x)
                {
                    cultistsToDespawn.Add(cultist); // mark for removal and deletion
                }
            }

            // remove marked cultists from list and destroy them
            foreach (NPCController cultist in cultistsToDespawn)
            {
                spawnedCultists.Remove(cultist);
                Destroy(cultist.gameObject);
            }
        }

        
    }

    /// <summary>
    /// spawns a cultist from the tent. also invokes the next cultist to be spawned if necessary
    /// </summary>
    private void SpawnCultistFromTent()
    {
        GameObject newCultist = Instantiate(cultistPrefab, cultistTentSpawnPoint);
        NPCController cultistController = newCultist.GetComponent<NPCController>();
        cultistController.FadeIn();
        cultistsSpawnedCount++;

        spawnedCultists.Add(cultistController);

        if (cultistsSpawnedCount < tentCultistCount)
        {
            // if more cultists need to be spawned, invoke the next one to spawn
            Invoke(nameof(SpawnCultistFromTent), cultistSpawnsDelay);
        }
    }

    void StartNextStage()
    {
        endingStage++;
    }

    /// <summary>
    /// if a stage has not been completed, this method registers it as complete and queues up the next stage with a given delay
    /// </summary>
    /// <param name="completedStage"></param>
    /// <param name="delay"></param>
    void CompleteStage(uint completedStage, float delay)
    {
        // checks if trying to complete the current stage
        if (completedStage == this.completedStage)
        {
            // completes current stage and invokes start of next stage
            this.completedStage++;
            Invoke(nameof(StartNextStage), delay);
        }
    }

}
