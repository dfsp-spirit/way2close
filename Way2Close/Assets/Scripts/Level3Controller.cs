using UnityEngine;
using System.Collections;

public class Level3Controller : LevelController
{

    private int numInitialEnemies = 10;
    private int numEnemiesAddedPerWave = 2;
    private int maxWave = 8;
    GameObject player;
    SpawnEnemies spawner;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        spawner = gameController.GetComponent<SpawnEnemies>();
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
            spawner.Spawn();
        }

        InvokeRepeating("NextWave", 10f, 10f);
    }

    void NextWave()
    {
        if (player.GetComponent<PlayerDie>().IsPlayerDead() || spawner.GetCurrentWave() >= this.maxWave)
        {
            return;
        }
        else
        {
            spawner.IncreaseWave();

            for (int i = 0; i < numEnemiesAddedPerWave; i++)
            {
                spawner.Spawn();
            }
        }
    }


    override protected void SetLevelEndedLevelControllerMode()
    {
        StopSpawning();
    }

    override protected int GetCurrentLevelIndex()
    {
        return 0;
    }

}
