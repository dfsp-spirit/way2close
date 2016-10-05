using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {

    SpawnEnemies spawner;
    Vector3 firstPos;
    Vector3 shiftVector;

    float RIGHT_SCREEN_BORDER = 5.0F;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        gameController = GameObject.Find("GameController");
        Invoke("ShowWelcomeText", 1.0F);
        spawner = gameController.GetComponent<SpawnEnemies>();        
    }

    override public float GetLevelDuration()
    {
        return 100.0F;
    }

    override public bool GetLevelHasFixedDuration()
    {
        return true;
    }

    override protected void SetLevelEndedLevelControllerMode()
    {
        // nothing to do for Level_1
    }

    override protected int GetCurrentLevelIndex()
    {
        return 1;
    }

    void ShowWelcomeText()
    {
        levelTextHeading.text = "Level 1";
        levelTextLine.text = "Get ready";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        spawner.SetCurrentWave(0);
        spawner.SetUseRandomEnemyFromPrefabs();

        Invoke("SpawnWave0", showNextPanelInTime);
    }

    // spawn a diagonal line (to the upper right)
    void SpawnWave0()
    {        
        firstPos = new Vector3(RIGHT_SCREEN_BORDER, -5.0F, 0.0F);
        shiftVector = new Vector3(1.0F, 1.0F, 0.0F);
        spawner.SpawnUpdwardsLine(firstPos, 8, shiftVector);        

        Invoke("SpawnWave1", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 parallel horizontal lines
    void SpawnWave1()
    {
        spawner.SetCurrentWave(1);

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, 2.0F, 0.0F);
        shiftVector = new Vector3(1.0F, 0.0F, 0.0F);
        spawner.SpawnUpdwardsLine(firstPos, 8, shiftVector);
        spawner.SpawnUpdwardsLine(new Vector3(RIGHT_SCREEN_BORDER, -2.0F, 0.0F), 8, shiftVector);

        Invoke("SpawnWave2", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 diagonal lines (to the upper right) above each other
    void SpawnWave2()
    {
        spawner.SetCurrentWave(2);

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, -5.0F, 0.0F);
        shiftVector = new Vector3(1.0F, 1.0F, 0.0F);
        spawner.SpawnUpdwardsLine(firstPos, 8, shiftVector);
        spawner.SpawnUpdwardsLine(VectorTools.PosAbove(firstPos, 5.0F), 8, shiftVector);

        Invoke("SpawnWave3", (showNextPanelInTime * 1.0F));
    }

    // spawn 3 vertical lines after each other. first starts at bottom, second start at half screen height, third at bottom.
    void SpawnWave3()
    {
        spawner.SetCurrentWave(3);

        float xdist = 5.0F;

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, -5.0F, 0.0F);
        shiftVector = new Vector3(0.0F, 1.5F, 0.0F);
        spawner.SpawnUpdwardsLine(firstPos, 5, shiftVector);
        spawner.SpawnUpdwardsLine(new Vector3(RIGHT_SCREEN_BORDER + xdist, 0.0F, 0.0F), 5, shiftVector);
        spawner.SpawnUpdwardsLine(new Vector3(RIGHT_SCREEN_BORDER + (xdist * 2), -5.0F, 0.0F), 5, shiftVector);

        //Invoke("SpawnWave3", showNextPanelInTime * 1.5F);
    }

}
