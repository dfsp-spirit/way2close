using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public GameObject tutorialPanel;
    public Text tutorialHeading;
    public Text tutorialLine;

    float showPanelDuration = 5.0F;
    float showNextPanelInTime = 8.0F;

    // Use this for initialization
    void Start () {
        tutorialPanel.SetActive(false);
        Invoke("showWelcomeText", 1.0F);
    }

    void showWelcomeText()
    {
        tutorialHeading.text = "Welcome to the Way2Close Tutorial";
        tutorialLine.text = "";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);

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

        Invoke("showFloorCeilingInfoText", showNextPanelInTime);
    }

    void showFloorCeilingInfoText()
    {
        tutorialHeading.text = "WARNING: Obstacles incoming.";
        tutorialLine.text = "Avoid all incoming obstacles.";
        ShowPanel();
        Invoke("HidePanel", showPanelDuration);
    }




    void ShowPanel()
    {
        tutorialPanel.SetActive(true);
    }

    void HidePanel()
    {
        tutorialPanel.SetActive(false);
    }
	
	
}
