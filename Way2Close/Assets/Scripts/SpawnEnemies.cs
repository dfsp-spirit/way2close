using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour {

    public Text levelText;
    public Text timeLeftText;
    public GameObject[] enemyTypePrefabs;
    public int numInitialEnemies;
    public int numMaxEnemies;
    public int currentWave;
    public int numEnemiesAddedPerWave;
    float timeSinceLevelLoaded;
    float maxLevelTime = 100F;
    float timeLeft;
    bool updateLevelTime;

	// Use this for initialization
	void Start () {
        currentWave = 0;
        updateLevelTime = true;
        timeLeft = maxLevelTime;
        levelText.text = "Wave " + currentWave.ToString();

        Invoke("StartEnemySpawning", 3.0F); // wait 3 secs, because the player needs some time to adapt to the level and also cannot move for 3 secs
    }

    void StartEnemySpawning()
    {
        for (int i = 0; i < numInitialEnemies; i++)
        {
            Spawn();
        }

        InvokeRepeating("NextWave", 10f, 10f);
    }

    void NextWave()
    {
        currentWave++;
        levelText.text = "Wave: " + currentWave.ToString();

        for (int i = 0; i < numEnemiesAddedPerWave; i++)
        {
            Spawn();
        }
    }
	
    void Spawn()
    {
        int objectRenderHeight = 150;
        int objectRenderWidth = 150;
        Vector3 spawnPos = new Vector3();
        spawnPos.x = Screen.width + objectRenderWidth + Random.Range(0, objectRenderWidth * 5);
        spawnPos.y = Random.Range(0 + objectRenderHeight, Screen.height - objectRenderHeight);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        int enemyTypeIndex = Random.Range(0, enemyTypePrefabs.Length);
        Instantiate(enemyTypePrefabs[enemyTypeIndex], worldPos, Quaternion.identity);
    }

    void StopSpawning()
    {
        CancelInvoke();
    }

    void StopUpdatingLevelTime()
    {
        updateLevelTime = false;
    }

    void Update()
    {
        if (updateLevelTime)
        {
            timeSinceLevelLoaded = Time.timeSinceLevelLoad;
            timeLeft = maxLevelTime - timeSinceLevelLoaded;
        }

        timeLeftText.text = "Time left: " + timeLeft.ToString("n2");
        timeLeftText.color = Color.white;
        if (timeLeft < (maxLevelTime * 0.5F))
        {
            timeLeftText.color = Color.yellow;
        }
        if (timeLeft < (maxLevelTime * 0.25F))
        {
            timeLeftText.color = Color.magenta;
        }
        if (timeLeft < (maxLevelTime * 0.1F))
        {
            timeLeftText.color = Color.red;
        }
        
    }
    
}
