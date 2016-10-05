using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour {

    public Text waveText;
    
    public GameObject[] enemyTypePrefabs;    
    private int currentWave;

    GameObject activePrefab;
    private bool randomEnemies;

    // Use this for initialization
    void Start()
    {
        currentWave = 0;        
        waveText.text = "Wave " + currentWave.ToString();
        SetUseRandomEnemyFromPrefabs();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    // wave can be set by the LevelXController script of the respective level
    public void SetCurrentWave(int wave)
    {
        currentWave = wave;
        UpdateWaveText();
    }

    public void SetUseRandomEnemyFromPrefabs()
    {
        this.randomEnemies = true;
    }

    public void SetActivePrefab(GameObject enemyPrefab)
    {
        this.randomEnemies = false;
        this.activePrefab = enemyPrefab;
    }

    public GameObject GetEnemyPrefabForSpawning()
    {
        if(this.randomEnemies)
        {
            int enemyTypeIndex = Random.Range(0, enemyTypePrefabs.Length);
            GameObject enemy = enemyTypePrefabs[enemyTypeIndex];
            return enemy;
        }
        else
        {
            return this.activePrefab;
        }
    }


    public void Spawn()
    {
        float objectRenderHeight;
        float objectRenderWidth;
        GameObject enemy = GetEnemyPrefabForSpawning();
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

    public void SpawnAtWorldPosition(Vector3 worldPos)
    {
        Instantiate(GetEnemyPrefabForSpawning(), worldPos, Quaternion.identity);
    }

    public void SpawnUpdwardsLine(Vector3 firstPos, int numObjects, Vector3 shiftVector)
    {
        List<Vector3> positions = new List<Vector3>();

        if(numObjects > 0)
        {
            for(int i = 0; i < numObjects; i++)
            {
                positions.Add(new Vector3(firstPos.x + (i * shiftVector.x), firstPos.y + (i * shiftVector.y), firstPos.z + (i * shiftVector.z)));
            }
        }

        SpawnFromWorldPositionsList(positions);
    }

    public void SpawnUpdwardsLineDefault()
    {
        Vector3 firstPos = new Vector3(5.0F, -5.0F, 0.0F);
        int numObjects = 8;
        Vector3 shiftVector = new Vector3(1.0F, 1.0F, 0.0F);
        SpawnUpdwardsLine(firstPos, numObjects, shiftVector);
    }

    private void SpawnFromWorldPositionsList(List<Vector3> positions)
    {
        foreach(Vector3 worldPos in positions)
        {
            Instantiate(GetEnemyPrefabForSpawning(), worldPos, Quaternion.identity);
        }
    }

    void StopSpawning()
    {
        CancelInvoke();
    }

    public void IncreaseWave()
    {
        currentWave++;
        UpdateWaveText();
    }


    private void UpdateWaveText()
    {
        waveText.text = "Wave: " + currentWave.ToString();
    }
    

    
    
}
