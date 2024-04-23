using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public List<AIController> enemies;
    public PlayerController player;
    public CameraController mainCamera;
    public HUD hud;
    [HideInInspector] public bool isPaused = false;
    
    public SpawnPoint[] spawnPoints;
    
    #region prefabs
    public GameObject prefabPlayerController;
    public GameObject prefabPlayerPawn;
    public List<GameObject> prefabsPossibleEnemies;
    #endregion
    
    //Awake is called before Start
    private void Awake()
    {
        if (instance == null)
        {
            //this is THE game manager
            instance = this;
        }
        else //this isn't THE game manager
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void FindCamera()
    {
        mainCamera = FindObjectOfType<CameraController>();
    }

    private void LoadSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length > 0)
        {
            return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform;
        }

        return null;
    }
    
    public Pawn SpawnPawn ()
    {
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        Pawn tempPawn = Instantiate(prefabPlayerPawn, randomSpawnPoint.transform.position, randomSpawnPoint.transform.rotation).GetComponent<Pawn>();

        return tempPawn;
    }
    
    public GameObject SpawnEnemy (GameObject enemyToSpawn)
    {
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        GameObject newEnemy = Instantiate(prefabsPossibleEnemies[UnityEngine.Random.Range(0,prefabsPossibleEnemies.Count)], randomSpawnPoint.transform.position, randomSpawnPoint.transform.rotation);
        
        enemies.Add(newEnemy.GetComponent<AIController>());

        return newEnemy;
    }
    
    public void SpawnPlayer ()
    {
        
        
        // spawn PlayerController if it doesn't already exist
        if (player == null)
        {
            player = Instantiate(prefabPlayerController).GetComponent<PlayerController>();
        }

        
        // spawn pawn and connect
        player.Possess(SpawnPawn());
        
        hud.player = player;
        hud.OnSpawn();
        hud.UpdateHUD();
        
        Health playerHealth = player.controlledPawn.GetComponent<Health>();
        if (playerHealth != null) {
            playerHealth.OnDeath.AddListener(OnPlayerDeath);
        }

        //set cam followtarget
        mainCamera.target = player.controlledPawn.transform;
        
        
    }
    
    public void RespawnPlayer()
    {
        //reenable input and such
        player.gameObject.SetActive(true);
        
        if (player.lives > 0)
        {
            Destroy(player.controlledPawn.gameObject);

            player.Unpossess();
            SpawnPlayer();
            
            player.lives--;
        } 
        // else call game over function
        else
        {
            DoGameOver();
        }
    }

    public void OnPlayerDeath()
    {
        player.gameObject.SetActive(false);
        
        RespawnPlayer();
    }
    
    public void StartGame()
    {
        // Connect to our camera
        FindCamera();

        // Load our spawn points
        LoadSpawnPoints();

        // Spawn player
        SpawnPlayer();
    }
    
    public void DoGameOver()
    {
    }
    
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
    }
    
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }
    
    public void TogglePause()
    {
        if (isPaused)
        {
            UnPause();
        } else
        {
            Pause();
        }
    }
}
