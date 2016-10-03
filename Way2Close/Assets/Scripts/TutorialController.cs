using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : LevelController {

    

    

    override public float GetLevelDuration()
    {
        return 100.0F;
    }

    override public bool GetLevelHasFixedDuration()
    {
        return false;
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();        
        Invoke("showWelcomeText", 1.0F);
        gameController.GetComponent<LevelUIController>().SendMessage("HideInGameHUD");
    }

    void showWelcomeText()
    {
        levelTextHeading.text = "Welcome to the Way2Close Tutorial";
        levelTextLine.text = "";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        //GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();

        Invoke("showControlInfoText", showNextPanelInTime);
    }

    void showControlInfoText()
    {
        levelTextHeading.text = "Controling your ship is a matter of thrust";
        levelTextLine.text = "Tap or hold the thrust control to fly up. Release to fall down.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showCoreInfoText", showNextPanelInTime);
    }

    void showCoreInfoText()
    {
        levelTextHeading.text = "Protect your vulnerable red core";
        levelTextLine.text = "Your ship can overlap with enemies and obstacles, but its core must not.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showEnemiesIncomingInfoText", showNextPanelInTime);
    }

    void showEnemiesIncomingInfoText()
    {
        levelTextHeading.text = "WARNING: Enemies incoming.";
        levelTextLine.text = "Avoid all incoming enemies.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        SpawnNumEnemies(3);

        Invoke("showWellDoneInfoText", (showNextPanelInTime * 1.5F));
    }

    void showWellDoneInfoText()
    {
        levelTextHeading.text = "Well done.";
        levelTextLine.text = "Now let's learn howto score more points.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showScoreInfoText", showNextPanelInTime);
    }

    void showScoreInfoText()
    {
        levelTextHeading.text = "Surviving is not enough.";
        levelTextLine.text = "You earn points for each second you survive. But increasing your multiplier is the key to glory.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        gameController.GetComponent<LevelUIController>().SendMessage("ShowInGameHUD");
        gameController.GetComponent<LevelUIController>().SendMessage("HideTimeAndWave");

        Invoke("showScoreMultiplierInfoText", showNextPanelInTime);
    }

    void showScoreMultiplierInfoText()
    {
        levelTextHeading.text = "Approach enemies to increase the multiplier.";
        levelTextLine.text = "A line will appear, indicating enemies that currently increase your multiplier.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        
        Invoke("spawnEnemyLines", showNextPanelInTime);
    }

    void spawnEnemyLines()
    {
        gameController.GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();
        Invoke("showTutorialEndText", (showNextPanelInTime * 2.0F));
    }

    void showTutorialEndText()
    {
        levelTextHeading.text = "Well done. Time to play the game!";
        levelTextLine.text = "This concludes the tutorial.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        Invoke("LoadMainMenu", (showPanelDuration * 1.0F));
    }


    

    void SpawnNumEnemies(int enemyCount)
    {
        for(int i = 0; i < enemyCount; i++)
        {
            gameController.GetComponent<SpawnEnemies>().SendMessage("Spawn");
        }        
    }


   
	
}
