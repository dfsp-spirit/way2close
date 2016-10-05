using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {

    SpawnEnemies spawner;
    Vector3 firstPos;

    float RIGHT_SCREEN_BORDER = 5.0F;
    private static Vector3 shiftVectorHorizontal = new Vector3(1.5F, 0.0F, 0.0F);
    private static Vector3 shiftVectorVerticalUp = new Vector3(0.0F, 1.5F, 0.0F);
    private static Vector3 shiftVectorVerticalDown = new Vector3(0.0F, -1.5F, 0.0F);
    private static Vector3 shiftVectorDiagonalUp = new Vector3(1.0F, 1.0F, 0.0F);
    private static Vector3 shiftVectorDiagonalDown = new Vector3(1.0F, -1.0F, 0.0F);

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
        spawner.SpawnLine(firstPos, 8, shiftVectorDiagonalUp);        

        Invoke("SpawnWave1", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 parallel horizontal lines
    void SpawnWave1()
    {
        spawner.SetCurrentWave(1);

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, 2.0F, 0.0F);        
        spawner.SpawnLine(firstPos, 8, shiftVectorHorizontal);
        spawner.SpawnLine(new Vector3(RIGHT_SCREEN_BORDER, -2.0F, 0.0F), 8, shiftVectorHorizontal);

        Invoke("SpawnWave2", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 diagonal lines (to the upper right) above each other
    void SpawnWave2()
    {
        spawner.SetCurrentWave(2);

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, -5.0F, 0.0F);
        spawner.SpawnLine(firstPos, 8, shiftVectorDiagonalUp);
        spawner.SpawnLine(VectorTools.PosAbove(firstPos, 5.0F), 8, shiftVectorDiagonalUp);

        Invoke("SpawnWave3", (showNextPanelInTime * 1.0F));
    }

    // spawn 3 vertical lines after each other. first starts at bottom, second start at half screen height, third at bottom again.
    void SpawnWave3()
    {
        spawner.SetCurrentWave(3);

        float xdist = 5.0F;

        firstPos = new Vector3(RIGHT_SCREEN_BORDER, -5.0F, 0.0F);
        spawner.SpawnLine(firstPos, 5, shiftVectorVerticalUp);
        spawner.SpawnLine(new Vector3(RIGHT_SCREEN_BORDER + xdist, 0.0F, 0.0F), 5, shiftVectorVerticalUp);
        spawner.SpawnLine(new Vector3(RIGHT_SCREEN_BORDER + (xdist * 2), -5.0F, 0.0F), 5, shiftVectorVerticalUp);

        Invoke("SpawnWave4", showNextPanelInTime * 1.5F);
    }

    // spawn 2 close parallel horizontal lines, and close other paths
    void SpawnWave4()
    {
        spawner.SetCurrentWave(4);

        Vector3 spacerX = new Vector3(1.5F, 0.0F, 0.0F);
        Vector3 spacerY = new Vector3(0.0F, 1.5F, 0.0F);

        Vector3 firstPosUpper = new Vector3(RIGHT_SCREEN_BORDER, 2.0F, 0.0F);
        Vector3 firstPosLower = new Vector3(RIGHT_SCREEN_BORDER, -2.0F, 0.0F);
        spawner.SpawnLine(firstPosUpper, 8, shiftVectorHorizontal);
        spawner.SpawnLine(firstPosLower, 8, shiftVectorHorizontal);

        spawner.SpawnLine(firstPosUpper + spacerY, 4, shiftVectorVerticalUp);
        spawner.SpawnLine(firstPosLower - spacerY, 4, shiftVectorVerticalDown);

        //Invoke("SpawnWave2", (showNextPanelInTime * 1.0F));
    }

}
