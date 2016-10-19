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
    float ySpawnBorderTop;
    float ySpawnBorderBottom;

    public float YSpawnBorderTop
    {
        get
        {
            return ySpawnBorderTop;
        }

        set
        {
            ySpawnBorderTop = value;
        }
    }

    public float YSpawnBorderBottom
    {
        get
        {
            return ySpawnBorderBottom;
        }

        set
        {
            ySpawnBorderBottom = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        YSpawnBorderTop = 0.0F;
        YSpawnBorderBottom = Screen.height;
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
        float randValXShift = Random.Range(0.0F, (Screen.width * 0.9F));

        spawnPos.x = Screen.width + objectRenderWidth + randValXShift;
        //Debug.Log("Spawning background enemy in main menu, adding random value " + randVal.ToString() + " to x coord. Resulting value is " + spawnPos.x.ToString() + ".");
        Debug.Log("Spawn borders y in screen coords are: ySpawnBorderTop=" + this.ySpawnBorderTop + ", this.ySpawnBorderBottom=" + this.ySpawnBorderBottom);
        
        float minSpawnYTop = getMinYSpawnPosAtTopForPrefab(enemy);
        float maxSpawnYBottom = getMaxYSpawnPosAtBottomForPrefab(enemy);

        Debug.Log("Spawning enemy type " + enemy.name + " in level between y coords " + minSpawnYTop + " and " + maxSpawnYBottom + ". Enemy render height is " + objectRenderHeight + ", render width is " + objectRenderHeight + ".");

        spawnPos.y = Random.Range(minSpawnYTop, maxSpawnYBottom);
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        Instantiate(enemy, worldPos, Quaternion.identity);
    }

    private float getMinYSpawnPosAtTopForPrefab(GameObject prefab)
    {
        float objectRenderHeight = prefab.GetComponent<Renderer>().bounds.size.y;
        return this.ySpawnBorderTop + objectRenderHeight;
    }

    private float getMaxYSpawnPosAtBottomForPrefab(GameObject prefab)
    {
        float objectRenderHeight = prefab.GetComponent<Renderer>().bounds.size.y;
        return this.ySpawnBorderBottom - objectRenderHeight;
    }

    public void SpawnAtWorldPosition(Vector3 worldPos)
    {
        Instantiate(GetEnemyPrefabForSpawning(), worldPos, Quaternion.identity);
    }

    public void SpawnLine(Vector3 firstPos, int numObjects, Vector3 shiftVector)
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
        SpawnLine(firstPos, numObjects, shiftVector);
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

    public Vector3 GetSpacerX()
    {
        return new Vector3(getWidthOfCurrentEnemy(), 0.0F, 0.0F);
    }

    public Vector3 GetSpacerY()
    {
        return new Vector3(0.0F, getHeightOfCurrentEnemy(), 0.0F); ;
    }

    public float getWidthOfCurrentEnemy()
    {
        return 1.5F;    //TODO: set based on active enemy prefab
    }

    public float getHeightOfCurrentEnemy()
    {
        return 1.5F;    //TODO: set based on active enemy prefab
    }

    public float getDiagonalLengthOfCurrentEnemy()
    {
        return 1.0F;    //TODO: set based on active enemy prefab
    }

    private void UpdateWaveText()
    {
        waveText.text = "Wave: " + currentWave.ToString();
    }

    public Vector3 GetShiftVectorHorizontal()
    {
        return new Vector3(getWidthOfCurrentEnemy(), 0.0F, 0.0F);
    }

    public Vector3 GetShiftVectorVerticalUp() {
        return new Vector3(0.0F, getHeightOfCurrentEnemy(), 0.0F);
    }

    public Vector3 GetShiftVectorVerticalDown()
    {
        return new Vector3(0.0F, -getHeightOfCurrentEnemy(), 0.0F);
    }

    public Vector3 GetShiftVectorDiagonalUp()
    {
        return new Vector3(getDiagonalLengthOfCurrentEnemy(), getDiagonalLengthOfCurrentEnemy(), 0.0F);
    }

    public Vector3 GetShiftVectorDiagonalDown()
    {
        return new Vector3(getDiagonalLengthOfCurrentEnemy(), -getDiagonalLengthOfCurrentEnemy(), 0.0F);
    }



}
