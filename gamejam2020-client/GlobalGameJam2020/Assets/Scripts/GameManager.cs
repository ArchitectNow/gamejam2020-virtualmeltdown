using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum LocalGameState
{
    Paused,
    Playing
}

public enum GameType
{
    SinglePlayer,
    GroupPlay,
    TeamVr
}

public class GameManager : MonoBehaviour
{
    private ColyseusManager colyseusManager;
    private AudioManager audioManager;
    
    public GameObject[] spawnPoints;
    private int lastSpawnPoint = 100;
    private Random rnd = new Random();
    
    public GameType gameType = GameType.TeamVr;
    
    public GameObject robotA;
    public GameObject robotB;
    public GameObject robotC;
    public GameObject robotD;
    public GameObject robotE;

    public int totalHeat = 0;

    public GameObject redWarnings;
    public GameObject blueWarnings;
    public GameObject greenWarnings;
    public GameObject yellowWarnings;

    public Material yellowMaterial;
    public Material blueMaterial;
    public Material redMaterial;
    public Material greenMaterial;

    public GameObject orangeFan;
    public GameObject greenFan;
    public GameObject brownFan;
    public GameObject tealFan;

    public bool orangeSmoking;
    public bool greenSmoking;
    public bool brownSmoking;
    public bool tealSmoking;
    
    public float timeLeft;
    public LocalGameState localGameState = LocalGameState.Paused;
    
    private Dictionary<string, GameObject> playerObjects = new Dictionary<string, GameObject>();

    public int DelayBeforeSmokeDamage = 30;
    public int SmokeDamageCheckInterval = 4;
    public int DelayBeforeRandomSmoke = 30;
    public int DelayBeforeRandomWarnings = 10;
    public int SmokeDamage = 5;
    
    // Start is called before the first frame update
    async Task Start()
    {
        colyseusManager = GameObject.FindObjectOfType<ColyseusManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        
        colyseusManager.ConnectToServer();
        await colyseusManager.CreateRoom("game");

        timeLeft = 60 * 4;
        
        colyseusManager.PlayerAdded += (sender, player) =>
        {
            var playerPrefab = GetPlayerPrefab(player);
            
            if (playerPrefab != null)
            {
                if (playerObjects.ContainsKey(player.name))
                {
                    Destroy(playerObjects[player.name]);
                    playerObjects.Remove(player.name);
                }

                var spawnPoint = GetNextSpawnPoint();

                if (spawnPoint == null)
                {
                    Debug.LogError("No spawn point found");
                    return;
                }
                
                var playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
                
                playerObjects.Add(player.name,playerObject);
                
                var playerScript = playerObject.GetComponent<PlayerController>();

                playerScript.player = player;
                
                audioManager.PlaySoundEffect(SoundEffectType.PlayerSpawn);

                if (playerObjects.Count == 1)
                {
                    StartGame(60*3); // Play for 3 minutes...
                }
                else
                {
                    Debug.Log($"Player joined '{player.name}'");
                }
            }
        };

        colyseusManager.PlayerRemoved += (sender, player) =>
        {
            if (playerObjects.ContainsKey(player.name))
            {
                Destroy(playerObjects[player.name]);
                playerObjects.Remove(player.name);
                
                Debug.Log($"Removed player '{player.name}'");
            }
        };

        colyseusManager.LeftRoom += code =>
        {
            foreach (var player in playerObjects)
            {
                Destroy(player.Value);
            }

            playerObjects.Clear();
        };
    }

    public int GetPlayerCount()
    {
        return playerObjects.Count;
    }

    public void StartGame(float Duration)  // Duration in seconds
    {
        timeLeft = Duration;
        localGameState = LocalGameState.Playing;
        Time.timeScale = 1;
        totalHeat = 1;
        
        InvokeRepeating("randomWarnings", DelayBeforeRandomWarnings, Random.Range(20f, 30f));
        InvokeRepeating("randomSmoking", DelayBeforeRandomSmoke, Random.Range(30f, 45f));
        InvokeRepeating("smokingDamage", DelayBeforeSmokeDamage, SmokeDamageCheckInterval);
        
        Debug.Log("Game Started!");
    }

    public void ResumeGame()
    {
        localGameState = LocalGameState.Playing;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        localGameState = LocalGameState.Paused;
        Time.timeScale = 0;
    }

    private void GameLost()
    {
        CancelInvoke();
        SceneManager.LoadScene("GameLost");
    }

    private void GameWon()
    {
        CancelInvoke();

        SceneManager.LoadScene("GameWon");
    }

    public void Update()
    {
        if (localGameState == LocalGameState.Playing && playerObjects.Count > 0)
        {
            timeLeft -= Time.deltaTime;
            
            if (timeLeft <= 0.0f)
            {
                GameWon();
            }

            if (totalHeat > 100)
            {
                GameLost();
            }
        }
    }

    public void DestroyPlayers()
    {
        foreach (var item in playerObjects)
        {
            Destroy(item.Value);
        }

        playerObjects.Clear();
    }

    public async Task Reconnect()
    {
        if (colyseusManager != null)
        {
            DestroyPlayers();
            colyseusManager.ConnectToServer();
            await colyseusManager.CreateRoom("game", null);

        }
    }

    private GameObject GetNextSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points specified");

            return null;
        }

        var index = -1;

        while (index < 0 || index == lastSpawnPoint)
        {
            index = Random.RandomRange(0, spawnPoints.Length);
        }

        if (index < 0 || index >= spawnPoints.Length)
        {
            Debug.Log("Invalid spawnpoint index");
            return null;
        }
        
        lastSpawnPoint = index;
        
        return spawnPoints[index];
    }

    private GameObject GetPlayerPrefab(Player player)
    {
        switch (player.type)
        {
            case "a":
                return robotA;
            case "b":
                return robotB;
            case "c":
                return robotC;
            case "d":
                return robotD;
            case "e":
                return robotE;
            default:
                return null;
        }
    }

    public int GetDifficultyLevel()
    {
        if (totalHeat < 10)
        {
            return 1;
        } else if (totalHeat < 25)
        {
            return 2;
        } else if (totalHeat < 35)
        {
            return 3;
        } else if (totalHeat < 50)
        {
            return 4;
        } else if (totalHeat < 70)
        {
            return 5;
        }

        return 6;
    }

    public void AdjustHeat(int adjustment)
    {
        totalHeat += adjustment;
    }
    
    public int GetOverallHeatLevel()
    {
        return totalHeat;
    }
    
    public void smokingDamage()
    {
        if (orangeSmoking)
        {
            this.totalHeat += SmokeDamage;
        }
        if (tealSmoking)
        {
            this.totalHeat += SmokeDamage;
        }
        if (brownSmoking)
        {
            this.totalHeat += SmokeDamage;
        }
        if (greenSmoking)
        {
            this.totalHeat += SmokeDamage;
        }
    }
    
    public void stopSmoking(string color)
    {
        if (color == "teal")
        {
            tealSmoking = false;
            ParticleSystem smoke1 = tealFan.GetComponent<ParticleSystem>();
            smoke1.Stop();
        }
        if (color == "green")
        {
            greenSmoking = false;
            ParticleSystem smoke2 = greenFan.GetComponent<ParticleSystem>();
            smoke2.Stop();
        }
        if (color == "orange")
        {
            orangeSmoking = false;
            ParticleSystem smoke3 = orangeFan.GetComponent<ParticleSystem>();
            smoke3.Stop();
        }
        if (color == "xmas")
        {
            brownSmoking = false;
            ParticleSystem smoke4 = brownFan.GetComponent<ParticleSystem>();
            smoke4.Stop();
        }
    }

    public void randomSmoking()
    {
        var randomNumber = Random.Range(1, 5);
        switch (randomNumber)
        {
            case 1:
                if (orangeSmoking != true)
                {
                    ParticleSystem smoke1 = orangeFan.GetComponent<ParticleSystem>();
                    orangeSmoking = true;
                    smoke1.Play();
                }

                break;
            case 2:
                if (greenSmoking != true)
                {
                    ParticleSystem smoke2 = greenFan.GetComponent<ParticleSystem>();
                    greenSmoking = true;
                    smoke2.Play();
                }

                break;
            case 3:
                if (brownSmoking != true)
                {
                    ParticleSystem smoke3 = brownFan.GetComponent<ParticleSystem>();
                    brownSmoking = true;
                    smoke3.Play();
                }

                break;
            case 4:
                if (tealSmoking != true)
                {
                    ParticleSystem smoke4 = tealFan.GetComponent<ParticleSystem>();
                    tealSmoking = true;
                    smoke4.Play();
                }

                break;
        }
    }

    public void randomWarnings()
    {
       var randomNumber = Random.Range(1, 5);

        switch(randomNumber)
        {
            case 1:
                if (redWarnings.GetComponent<WarningController>().needsFixing)
                {
                    randomWarnings();
                    return;
                }
             for(var i = 0; i < 3; i ++)
                {
                    var randomMaterial = Random.Range(1, 5);
                    switch (randomMaterial)
                    {
                        case 1:
                            redWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = redMaterial;
                            redWarnings.GetComponent<WarningController>().requiredRed += 1;
                            redWarnings.GetComponent<WarningController>().setColor(i, 1);
                            break;
                        case 2:
                            redWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = greenMaterial;
                            redWarnings.GetComponent<WarningController>().requiredGreen += 1;
                            redWarnings.GetComponent<WarningController>().setColor(i, 2);
                            break;
                        case 3:
                            redWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = blueMaterial;
                            redWarnings.GetComponent<WarningController>().requiredBlue += 1;
                            redWarnings.GetComponent<WarningController>().setColor(i, 3);
                            break;
                        case 4:
                            redWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = yellowMaterial;
                            redWarnings.GetComponent<WarningController>().requiredYellow += 1;
                            redWarnings.GetComponent<WarningController>().setColor(i, 4);
                            break;
                    }
                }
             redWarnings.GetComponent<WarningController>().needsFixing = true;
             break;
            case 2:
                if (blueWarnings.GetComponent<WarningController>().needsFixing)
                {
                    randomWarnings();
                    return;
                }
                for (var i = 0; i < 3; i++)
                {
                    var randomMaterial = Random.Range(1, 5);
                    switch (randomMaterial)
                    {
                        case 1:
                            blueWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = redMaterial;
                            blueWarnings.GetComponent<WarningController>().requiredRed += 1;
                            blueWarnings.GetComponent<WarningController>().setColor(i, 1);
                            break;
                        case 2:
                            blueWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = greenMaterial;
                            blueWarnings.GetComponent<WarningController>().requiredGreen += 1;
                            blueWarnings.GetComponent<WarningController>().setColor(i, 2);
                            break;
                        case 3:
                            blueWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = blueMaterial;
                            blueWarnings.GetComponent<WarningController>().requiredBlue += 1;
                            blueWarnings.GetComponent<WarningController>().setColor(i, 3);
                            break;
                        case 4:
                            blueWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = yellowMaterial;
                            blueWarnings.GetComponent<WarningController>().requiredYellow += 1;
                            blueWarnings.GetComponent<WarningController>().setColor(i, 4);
                            break;
                    }
                }
                blueWarnings.GetComponent<WarningController>().needsFixing = true;
                break;
            case 3:
                if (yellowWarnings.GetComponent<WarningController>().needsFixing)
                {
                    randomWarnings();
                    return;
                }
                for (var i = 0; i < 3; i++)
                {
                    var randomMaterial = Random.Range(1, 5);
                    switch (randomMaterial)
                    {
                        case 1:
                            yellowWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = redMaterial;
                            yellowWarnings.GetComponent<WarningController>().requiredRed += 1;
                            yellowWarnings.GetComponent<WarningController>().setColor(i, 1);
                            break;
                        case 2:
                            yellowWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = greenMaterial;
                            yellowWarnings.GetComponent<WarningController>().requiredGreen += 1;
                            yellowWarnings.GetComponent<WarningController>().setColor(i, 2);
                            break;
                        case 3:
                            yellowWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = blueMaterial;
                            yellowWarnings.GetComponent<WarningController>().requiredBlue += 1;
                            yellowWarnings.GetComponent<WarningController>().setColor(i, 3);
                            break;
                        case 4:
                            yellowWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = yellowMaterial;
                            yellowWarnings.GetComponent<WarningController>().requiredYellow += 1;
                            yellowWarnings.GetComponent<WarningController>().setColor(i, 4);
                            break;
                    }
                }
                yellowWarnings.GetComponent<WarningController>().needsFixing = true;
                break;
            case 4:
                if (greenWarnings.GetComponent<WarningController>().needsFixing)
                {
                    randomWarnings();
                    return;
                }
                for (var i = 0; i < 3; i++)
                {
                    var randomMaterial = Random.Range(1, 5);
                    switch (randomMaterial)
                    {
                        case 1:
                            greenWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = redMaterial;
                            greenWarnings.GetComponent<WarningController>().requiredRed += 1;
                            greenWarnings.GetComponent<WarningController>().setColor(i, 1);
                            break;
                        case 2:
                            greenWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = greenMaterial;
                            greenWarnings.GetComponent<WarningController>().requiredGreen += 1;
                            greenWarnings.GetComponent<WarningController>().setColor(i, 2);
                            break;
                        case 3:
                            greenWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = blueMaterial;
                            greenWarnings.GetComponent<WarningController>().requiredBlue += 1;
                            greenWarnings.GetComponent<WarningController>().setColor(i, 3);
                            break;
                        case 4:
                            greenWarnings.transform.GetChild(i).GetComponent<MeshRenderer>().material = yellowMaterial;
                            greenWarnings.GetComponent<WarningController>().requiredYellow += 1;
                            greenWarnings.GetComponent<WarningController>().setColor(i, 4);
                            break;
                    }
                }
                greenWarnings.GetComponent<WarningController>().needsFixing = true;
                break;

        }
    }

    public void Deposit(string color, int amount)
    {
        switch (color)
        {
            case "red":
                redWarnings.GetComponent<WarningController>().applyFix("red");
                break;
            case "green":
                redWarnings.GetComponent<WarningController>().applyFix("green");
                break;
            case "blue":
                redWarnings.GetComponent<WarningController>().applyFix("blue");
                break;
            case "yellow":
                redWarnings.GetComponent<WarningController>().applyFix("yellow");
                break;
            default:
                Debug.LogError("Invalid deposit type: " + color);
                break;
        }
    }
}
