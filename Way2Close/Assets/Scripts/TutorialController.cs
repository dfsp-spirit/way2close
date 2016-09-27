using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialPanel;
    public Text tutorialHeading;
    public Text tutorialLine;
    Text[] texts;

    float showPanelDuration = 5.0F;
    float showNextPanelInTime = 8.0F;
    float uiFadeDuration = 1.0F;

    // Use this for initialization
    void Start () {
        texts = tutorialPanel.GetComponentsInChildren<Text>();
        tutorialPanel.SetActive(false);
        Invoke("showWelcomeText", 1.0F);
        GetComponent<LevelUIController>().SendMessage("HideInGameHUD");
    }

    void showWelcomeText()
    {
        tutorialHeading.text = "Welcome to the Way2Close Tutorial";
        tutorialLine.text = "";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();

        Invoke("showControlInfoText", showNextPanelInTime);
    }

    void showControlInfoText()
    {
        tutorialHeading.text = "Controling your ship is a matter of thrust";
        tutorialLine.text = "Tap or hold the thrust control to fly up. Release to fall down.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showCoreInfoText", showNextPanelInTime);
    }

    void showCoreInfoText()
    {
        tutorialHeading.text = "Protect your vulnerable red core";
        tutorialLine.text = "Your ship can overlap with enemies and obstacles, but its core must not.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showEnemiesIncomingInfoText", showNextPanelInTime);
    }

    void showEnemiesIncomingInfoText()
    {
        tutorialHeading.text = "WARNING: Enemies incoming.";
        tutorialLine.text = "Avoid all incoming enemies.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        SpawnNumEnemies(3);

        Invoke("showWellDoneInfoText", (showNextPanelInTime * 2.0F));
    }

    void showWellDoneInfoText()
    {
        tutorialHeading.text = "Well done.";
        tutorialLine.text = "Now let's learn howto score more points.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        Invoke("showScoreInfoText", showNextPanelInTime);
    }

    void showScoreInfoText()
    {
        tutorialHeading.text = "Surviving is not enough.";
        tutorialLine.text = "You earn points for each second you survive. But increasing your multiplier is the key to glory.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

        GetComponent<LevelUIController>().SendMessage("ShowInGameHUD");
        GetComponent<LevelUIController>().SendMessage("HideTimeAndWave");

        Invoke("showScoreMultiplierInfoText", showNextPanelInTime);
    }

    void showScoreMultiplierInfoText()
    {
        tutorialHeading.text = "Approach enemies to increase the multiplier.";
        tutorialLine.text = "A line will appear, indicating enemies that currently increase your multiplier.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        //SpawnNumEnemies(3);
        GetComponent<SpawnEnemies>().SpawnUpdwardsLineDefault();

        Invoke("showTutorialEndText", (showNextPanelInTime * 2.0F));
    }

    void showTutorialEndText()
    {
        tutorialHeading.text = "Well done. Time to play the game!";
        tutorialLine.text = "This concludes the tutorial.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
        Invoke("LoadMainMenu", (showPanelDuration * 1.0F));
    }


    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SpawnNumEnemies(int enemyCount)
    {
        for(int i = 0; i < enemyCount; i++)
        {
            GetComponent<SpawnEnemies>().SendMessage("Spawn");
        }        
    }


    void ShowPanel()
    {
        tutorialPanel.SetActive(true);

        foreach (Text t in texts)
        {
            t.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
            t.CrossFadeAlpha(1f, uiFadeDuration, false);
        }
    }

    void HidePanel()
    {        
        foreach (Text t in texts)
        {
            t.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            t.CrossFadeAlpha(0.1f, uiFadeDuration, false);
        }


        Invoke("DeactivatePanel", uiFadeDuration);
    }
	
    void DeactivatePanel()
    {
        tutorialPanel.SetActive(false);
    }
	
}
