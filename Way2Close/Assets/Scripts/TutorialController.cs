using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
