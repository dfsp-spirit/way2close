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
        CancelInvoke();
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

        spawner.SetCurrentWave(0);
        spawner.SetUseRandomEnemyFromPrefabs();

        //Invoke("SpawnWave0", showNextPanelInTime);
    }
}