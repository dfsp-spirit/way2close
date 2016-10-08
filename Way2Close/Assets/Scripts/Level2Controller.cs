using UnityEngine;
using System.Collections;

public class Level2Controller : LevelController
{

    SpawnEnemies spawner;
    Vector3 firstPos;

    protected float WORLD_X_RIGHT_BORDER = 5.0F;
    protected float WORLD_X_LEFT_BORDER = -5.0F;
    protected float WORLD_X_CENTER = 0.0F;
    protected float WORLD_Y_TOP = +5.0F;
    protected float WORLD_Y_BOTTOM = -5.0F;
    protected float WORLD_Y_CENTER = 0.0F;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        gameController = GameObject.Find("GameController");
        Invoke("ShowWelcomeText", 1.0F);
        spawner = gameController.GetComponent<SpawnEnemies>();
        spawner.SetCurrentWave(0);
    }

    override public float GetLevelDuration()
    {
        return 80.0F;
    }

    override public bool GetLevelHasFixedDuration()
    {
        return true;
    }

    override protected void SetLevelEndedLevelControllerMode()
    {
        StopSpawning();
    }

    override protected int GetCurrentLevelIndex()
    {
        return 2;
    }



    void ShowWelcomeText()
    {
        levelTextHeading.text = "Level " + GetCurrentLevelIndex() + ": " + GetLevelFancyName();
        levelTextLine.text = "Get ready";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        spawner.SetUseRandomEnemyFromPrefabs();

        Invoke("SpawnWave0", showNextPanelInTime);
    }

    void SpawnWave0()
    {
        spawner.SetCurrentWave(0);

        Debug.Log("Spawning obstacle.");
        obstacleSpawner.ObstacleSpeed = 2.0F;

        obstacleSpawner.SpawnPosition = new Vector3(5.0F, 0.0F, 0.0F);
        obstacleSpawner.SpawnObstaclePolygon("Obstacle", ObstacleSpawner.verticesTrapez);

        obstacleSpawner.SpawnPosition = new Vector3(5.0F, 2.0F, 0.0F);
        obstacleSpawner.SpawnObstaclePolygon("Obstacle", ObstacleSpawner.verticesTriangle);

        obstacleSpawner.SpawnPosition = new Vector3(5.0F, -2.0F, 0.0F);
        obstacleSpawner.SpawnObstaclePolygon("Obstacle", ObstacleSpawner.verticesRectangle);

        Invoke("SpawnWave1", showNextPanelInTime);
    }

    void SpawnWave1()
    {
        spawner.SetCurrentWave(1);
    }
}