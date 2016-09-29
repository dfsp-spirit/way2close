using UnityEngine;
using System.Collections;

public class Level0Controller : LevelController {

    private int numInitialEnemies = 6;
    private int numEnemiesAddedPerWave = 2;
    GameObject gameController;
    
    void Start () {
        gameController = GameObject.Find("GameController");
        Invoke("StartEnemySpawning", 3.0F);
    }

    override public float GetLevelDuration()
    {
        return 100.0F;
    }

    override public bool GetLevelHasFixedDuration()
    {
        return true;
    }

    void StartEnemySpawning()
    {
        for (int i = 0; i < numInitialEnemies; i++)
        {
            gameController.GetComponent<SpawnEnemies>().Spawn();
        }

        InvokeRepeating("NextWave", 10f, 10f);
    }

    void NextWave()
    {
        gameController.GetComponent<SpawnEnemies>().IncreaseWave();               

        for (int i = 0; i < numEnemiesAddedPerWave; i++)
        {
            gameController.GetComponent<SpawnEnemies>().Spawn();
        }
    }

    void StopSpawning()
    {
        CancelInvoke();
    }
}
