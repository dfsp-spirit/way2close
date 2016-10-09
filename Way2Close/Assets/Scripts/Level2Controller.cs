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
    protected float WORLD_UPPER_BORDER_EDGE_Y = 4.15F;
    protected float WORLD_LOWER_BORDER_EDGE_Y = -4.15F;
    



    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        gameController = GameObject.Find("GameController");
        Invoke("ShowWelcomeText", 1.0F);
        spawner = gameController.GetComponent<SpawnEnemies>();
        spawner.SetCurrentWave(0);

        obstacleSpawner.ObstacleSpeed = 2.0F;
        obstacleSpawner.UpperBoarderYPos = WORLD_UPPER_BORDER_EDGE_Y;
        obstacleSpawner.LowerBoarderYPos = WORLD_LOWER_BORDER_EDGE_Y;

        GameObject borderTop = GameObject.Find("BorderTop");
        GameObject borderBottom = GameObject.Find("BorderBottom");
        float borderTopMinYPos = borderTop.GetComponent<Renderer>().bounds.min.y;
        float borderBottomMaxYPos = borderBottom.GetComponent<Renderer>().bounds.max.y;
        Debug.Log("BorderTop position is " + borderTop.transform.position.ToString() + ", min y pos is " + borderTopMinYPos.ToString("n2") + ".");
        Debug.Log("BorderBottom position is " + borderBottom.transform.position.ToString() + ", max y pos is " + borderBottomMaxYPos.ToString("n2") + ".");
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

        Invoke("SpawnWave1", showNextPanelInTime);
    }

    void SpawnWave0()
    {
        spawner.SetCurrentWave(0);

        Debug.Log("Spawning obstacle.");        

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

        obstacleSpawner.SpawnPosition = new Vector3(0.0F, 0.0F, 0.0F);
        obstacleSpawner.SpawnPolyAtBottomBorderFromTo("Obstacle", new Vector2(0.0F, 0.0F), new Vector2(2.0F, 2.0F));
        obstacleSpawner.SpawnPolyAtBottomBorderFromTo("Obstacle", new Vector2(2.0F, 2.0F), new Vector2(4.0F, 2.0F));
        obstacleSpawner.SpawnPolyAtBottomBorderFromTo("Obstacle", new Vector2(4.0F, 2.0F), new Vector2(7.0F, 1.0F));
    }
}