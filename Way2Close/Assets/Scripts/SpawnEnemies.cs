using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    void Start()
    {
        currentWave = 0;
        updateLevelTime = true;
        timeLeft = maxLevelTime;
        levelText.text = "Wave " + currentWave.ToString();

        if (SceneManager.GetActiveScene().name == "Level_0") {
            Invoke("StartEnemySpawning", 3.0F); // wait 3 secs, because the player needs some time to adapt to the level and also cannot move for 3 secs
        }
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

    void SpawnAtWorldPosition(Vector3 worldPos)
    {
        int enemyTypeIndex = Random.Range(0, enemyTypePrefabs.Length);
        Instantiate(enemyTypePrefabs[enemyTypeIndex], worldPos, Quaternion.identity);
    }

    public void SpawnUpdwardsLine(GameObject prefab, Vector3 firstPos, int numObjects, Vector3 shiftVector)
    {
        List<Vector3> positions = new List<Vector3>();

        if(numObjects > 0)
        {
            for(int i = 0; i < numObjects; i++)
            {
                positions.Add(new Vector3(firstPos.x + (i * shiftVector.x), firstPos.y + (i * shiftVector.y), firstPos.z + (i * shiftVector.z)));
            }
        }

        spawnFromWorldPositionsList(positions, prefab);
    }

    public void SpawnUpdwardsLineDefault()
    {
        GameObject prefab = enemyTypePrefabs[0];
        Vector3 firstPos = new Vector3(5.0F, -5.0F, 0.0F);
        int numObjects = 8;
        Vector3 shiftVector = new Vector3(1.0F, 1.0F, 0.0F);
        SpawnUpdwardsLine(prefab, firstPos, numObjects, shiftVector);
    }

    private void spawnFromWorldPositionsList(List<Vector3> positions, GameObject prefab)
    {
        foreach(Vector3 worldPos in positions)
        {
            Instantiate(prefab, worldPos, Quaternion.identity);
        }
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
