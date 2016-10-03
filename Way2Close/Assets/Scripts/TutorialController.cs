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
        Invoke("ShowWelcomeText", 1.0F);
        gameController.GetComponent<LevelUIController>().SendMessage("HideInGameHUD");
    }

    void ShowWelcomeText()
    {
        levelTextHeading.text = "Welcome to the Way2Close Tutorial";
        levelTextLine.text = "";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        //GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();

        Invoke("ShowControlInfoText", showNextPanelInTime);
    }

    void ShowControlInfoText()
    {
        levelTextHeading.text = "Controling your ship is a matter of thrust";
        levelTextLine.text = "Tap or hold the thrust control to fly up. Release to fall down.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("ShowCoreInfoText", showNextPanelInTime);
    }

    void ShowCoreInfoText()
    {
        levelTextHeading.text = "Protect your vulnerable red core";
        levelTextLine.text = "Your ship can overlap with enemies and obstacles, but its core must not.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("ShowEnemiesIncomingInfoText", showNextPanelInTime);
    }

    void ShowEnemiesIncomingInfoText()
    {
        levelTextHeading.text = "WARNING: Enemies incoming.";
        levelTextLine.text = "Avoid all incoming enemies.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        SpawnNumEnemies(3);

        Invoke("ShowWellDoneInfoText", (showNextPanelInTime * 1.5F));
    }

    void ShowWellDoneInfoText()
    {
        levelTextHeading.text = "Well done.";
        levelTextLine.text = "Now let's learn howto score more points.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("ShowScoreInfoText", showNextPanelInTime);
    }

    void ShowScoreInfoText()
    {
        levelTextHeading.text = "Surviving is not enough.";
        levelTextLine.text = "You earn points for each second you survive. But increasing your multiplier is the key to glory.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        gameController.GetComponent<LevelUIController>().SendMessage("ShowInGameHUD");
        gameController.GetComponent<LevelUIController>().SendMessage("HideTimeAndWave");

        Invoke("ShowScoreMultiplierInfoText", showNextPanelInTime);
    }

    void ShowScoreMultiplierInfoText()
    {
        levelTextHeading.text = "Approach enemies to increase the multiplier.";
        levelTextLine.text = "A line will appear, indicating enemies that currently increase your multiplier.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        
        Invoke("SpawnEnemyLines", showNextPanelInTime);
    }

    void SpawnEnemyLines()
    {
        gameController.GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();
        Invoke("ShowTutorialEndText", (showNextPanelInTime * 2.0F));
    }

    void ShowTutorialEndText()
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

    override protected void SetLevelEndedLevelControllerMode()
    {
        // nothing to do for tutorial
    }

    override protected int GetCurrentLevelIndex()
    {
        return -1;
    }


}
