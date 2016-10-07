using UnityEngine;
using System.Collections;

public class Level0Controller : LevelController {

    private int numInitialEnemies = 6;
    private int numEnemiesAddedPerWave = 2;

    protected override void Start () {
        base.Start();
        Invoke("ShowWelcomeText", 1.0F);
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

    void ShowWelcomeText()
    {
        levelTextHeading.text = "Level " + GetCurrentLevelIndex() + ": " + GetLevelFancyName();
        levelTextLine.text = "Get ready";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
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

    override protected void SetLevelEndedLevelControllerMode()
    {
        // nothing to do for Level_0
    }

    override protected int GetCurrentLevelIndex()
    {
        return 0;
    }

}
