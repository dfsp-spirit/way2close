using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {


    // Use this for initialization
    protected override void Start () {
        base.Start();
        gameController = GameObject.Find("GameController");
        Invoke("ShowWelcomeText", 1.0F);
        gameController.GetComponent<SpawnEnemies>().Spawn();
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
    }

}
