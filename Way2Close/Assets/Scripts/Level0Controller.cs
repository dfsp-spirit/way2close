using UnityEngine;
using System.Collections;

public class Level0Controller : MonoBehaviour {

    private int numInitialEnemies;
    private int numEnemiesAddedPerWave;

    
    void Start () {
        Invoke("StartEnemySpawning", 3.0F);
    }		

    void StartEnemySpawning()
    {
        for (int i = 0; i < numInitialEnemies; i++)
        {
            GetComponent<SpawnEnemies>().Spawn();
        }

        InvokeRepeating("NextWave", 10f, 10f);
    }

    void NextWave()
    {
        GetComponent<SpawnEnemies>().IncreaseWave();
        int currentWave = GetComponent<SpawnEnemies>().GetCurrentWave();
        
        

        for (int i = 0; i < numEnemiesAddedPerWave; i++)
        {
            GetComponent<SpawnEnemies>().Spawn();
        }
    }

    void StopSpawning()
    {
        CancelInvoke();
    }
}
