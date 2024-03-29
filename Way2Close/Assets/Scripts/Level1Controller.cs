﻿using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {

    SpawnEnemies spawner;
    Vector3 firstPos;

    protected float WORLD_X_CENTER = 0.0F;
    protected float WORLD_Y_CENTER = 0.0F;

    // Use this for initialization
    protected override void Start () {
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
        StopSpawning();
    }

    override protected int GetCurrentLevelIndex()
    {
        return 1;
    }

    

    void ShowWelcomeText()
    {
        levelTextHeading.text = "Level " + GetCurrentLevelIndex() + ": " + GetLevelFancyName();
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
        spawner.SetCurrentWave(0);

        firstPos = new Vector3(this.rightScreenBorderWorldPos, -5.0F, 0.0F);
        //firstPos = new Vector3(x, -5.0F, 0.0F);
        spawner.SpawnLine(firstPos, 8, spawner.GetShiftVectorDiagonalUp());        

        Invoke("SpawnWave1", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 parallel horizontal lines
    void SpawnWave1()
    {
        spawner.SetCurrentWave(1);

        firstPos = new Vector3(this.rightScreenBorderWorldPos, 2.0F, 0.0F);        
        spawner.SpawnLine(firstPos, 8, spawner.GetShiftVectorHorizontal());
        spawner.SpawnLine(new Vector3(this.rightScreenBorderWorldPos, -2.0F, 0.0F), 8, spawner.GetShiftVectorHorizontal());

        Invoke("SpawnWave2", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 diagonal lines (to the upper right) above each other
    void SpawnWave2()
    {
        spawner.SetCurrentWave(2);

        firstPos = new Vector3(this.rightScreenBorderWorldPos, this.bottomScreenBorderWorldPos, 0.0F);
        spawner.SpawnLine(firstPos, 8, spawner.GetShiftVectorDiagonalUp());
        spawner.SpawnLine(VectorTools.PosAbove(firstPos, 5.0F), 8, spawner.GetShiftVectorDiagonalUp());

        Invoke("SpawnWave3", (showNextPanelInTime * 1.0F));
    }

    // spawn 3 vertical lines after each other. first starts at bottom, second start at half screen height, third at bottom again.
    void SpawnWave3()
    {
        spawner.SetCurrentWave(3);

        float xDistBetweenLines = 7.0F;

        firstPos = new Vector3(this.rightScreenBorderWorldPos, this.bottomScreenBorderWorldPos, 0.0F);
        spawner.SpawnLine(firstPos, 5, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(new Vector3(this.rightScreenBorderWorldPos + xDistBetweenLines, 0.0F, 0.0F), 5, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(new Vector3(this.rightScreenBorderWorldPos + (xDistBetweenLines * 2), -5.0F, 0.0F), 5, spawner.GetShiftVectorVerticalUp());

        Invoke("SpawnWave4", showNextPanelInTime * 1.5F);
    }
    

    // spawn 2 close parallel horizontal lines, and close other paths
    void SpawnWave4()
    {
        spawner.SetCurrentWave(4);

        Vector3 firstPosUpper = new Vector3(this.rightScreenBorderWorldPos, 2.0F, 0.0F);
        Vector3 firstPosLower = new Vector3(this.rightScreenBorderWorldPos, -2.0F, 0.0F);
        spawner.SpawnLine(firstPosUpper, 8, spawner.GetShiftVectorHorizontal());
        spawner.SpawnLine(firstPosLower, 8, spawner.GetShiftVectorHorizontal());

        spawner.SpawnLine(firstPosUpper + spawner.GetSpacerY(), 4, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(firstPosLower - spawner.GetSpacerY(), 4, spawner.GetShiftVectorVerticalDown());

        Invoke("SpawnWave5", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 even closer parallel horizontal lines, and close other paths
    void SpawnWave5()
    {
        spawner.SetCurrentWave(5);

        Vector3 firstPosUpper = new Vector3(this.rightScreenBorderWorldPos, 1.5F, 0.0F);
        Vector3 firstPosLower = new Vector3(this.rightScreenBorderWorldPos, -1.5F, 0.0F);
        spawner.SpawnLine(firstPosUpper, 8, spawner.GetShiftVectorHorizontal());
        spawner.SpawnLine(firstPosLower, 8, spawner.GetShiftVectorHorizontal());

        spawner.SpawnLine(firstPosUpper + spawner.GetSpacerY(), 4, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(firstPosLower - spawner.GetSpacerY(), 4, spawner.GetShiftVectorVerticalDown());

        Invoke("SpawnWave6", (showNextPanelInTime * 1.0F));
    }

    // spawn 2 diagonal lines (to the lower right) above each other
    void SpawnWave6()
    {
        spawner.SetCurrentWave(6);

        firstPos = new Vector3(this.rightScreenBorderWorldPos, this.topScreenBorderWorldPos, 0.0F);
        spawner.SpawnLine(firstPos, 8, spawner.GetShiftVectorDiagonalDown());
        spawner.SpawnLine(VectorTools.PosBelow(firstPos, 5.0F), 8, spawner.GetShiftVectorDiagonalDown());

        Invoke("SpawnWave7", (showNextPanelInTime * 1.0F));
    }

    // spawn 3 gates, where a gate is a vertical line with only one spot where the player can get through
    void SpawnWave7()
    {
        spawner.SetCurrentWave(7);

        float xDistBetweenGates = 7.0F;

        firstPos = new Vector3(this.rightScreenBorderWorldPos, WORLD_Y_CENTER, 0.0F);
        spawner.SpawnLine(firstPos, 5, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(firstPos - (spawner.GetSpacerY() * 2), 5, spawner.GetShiftVectorVerticalDown());

        firstPos = new Vector3(this.rightScreenBorderWorldPos + (xDistBetweenGates * 1), WORLD_Y_CENTER + spawner.GetSpacerY().y, 0.0F);
        spawner.SpawnLine(firstPos, 5, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(firstPos - (spawner.GetSpacerY() * 2), 5, spawner.GetShiftVectorVerticalDown());

        firstPos = new Vector3(this.rightScreenBorderWorldPos + (xDistBetweenGates * 2), WORLD_Y_CENTER - spawner.GetSpacerY().y, 0.0F);
        spawner.SpawnLine(firstPos, 5, spawner.GetShiftVectorVerticalUp());
        spawner.SpawnLine(firstPos - (spawner.GetSpacerY() * 2), 5, spawner.GetShiftVectorVerticalDown());

        Invoke("EndLevel", (showNextPanelInTime * 2.0F));
    }

}
