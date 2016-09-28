using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour {

    public Text levelText;
    
    public GameObject[] enemyTypePrefabs;
    public int numInitialEnemies;
    public int numMaxEnemies;
    public int currentWave;
    public int numEnemiesAddedPerWave;
    

    // Use this for initialization
    void Start()
    {
        currentWave = 0;        
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
        float objectRenderHeight;
        float objectRenderWidth;
        int enemyTypeIndex = Random.Range(0, enemyTypePrefabs.Length);
        GameObject enemy = enemyTypePrefabs[enemyTypeIndex];
        objectRenderWidth = enemy.GetComponent<Renderer>().bounds.size.x;
        objectRenderHeight = enemy.GetComponent<Renderer>().bounds.size.y;

        Vector3 spawnPos = new Vector3();
        float randVal = Random.Range(0.0F, (Screen.width * 0.9F));

        spawnPos.x = Screen.width + objectRenderWidth + randVal;
        //Debug.Log("Spawning background enemy in main menu, adding random value " + randVal.ToString() + " to x coord. Resulting value is " + spawnPos.x.ToString() + ".");
        spawnPos.y = Random.Range(0 + objectRenderHeight, Screen.height - objectRenderHeight);
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        Instantiate(enemy, worldPos, Quaternion.identity);
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

    

    
    
}
