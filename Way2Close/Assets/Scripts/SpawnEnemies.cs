using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour {

    public Text levelText;
    public GameObject[] enemyTypePrefabs;
    public int numInitialEnemies;
    public int numMaxEnemies;
    int currentWave;
    public int numEnemiesAddedPerWave;

	// Use this for initialization
	void Start () {
        currentWave = 0;
        levelText.text = "Wave " + currentWave.ToString();

        for(int i = 0; i < numInitialEnemies; i++)
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
}
