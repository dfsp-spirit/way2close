using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour {

    public GameObject uiPanelDeathMenu;
    public GameObject uiPanelLevelDone;
    public Text uiTextMultiplier;
    public Text uiTextTime;
    public Text uiTextScore;
    public Text uiTextWave;
    public Text uiTextLevelDoneTitle;
    public Text uiTextLevelDoneLine1;
    public Text uiTextLevelDoneLine2;
    public Button buttonLevelDonePlayNext;
    public Button buttonLevelDoneToMainMenu;

    // Use this for initialization
    void Start () {
        SetShowHighScorePanel(false);
        SetShowLevelDonePanel(false);
        SetShowInGameHUD(true);
    }
		
    void ShowInGameHUD()
    {
        SetShowInGameHUD(true);
    }

    void HideInGameHUD()
    {
        SetShowInGameHUD(false);
    }

    void HideTimeAndWave()
    {
        uiTextTime.gameObject.SetActive(false);
        uiTextWave.gameObject.SetActive(false);
    }

    private void SetShowInGameHUD(bool state)
    {
        uiTextMultiplier.gameObject.SetActive(state);
        uiTextTime.gameObject.SetActive(state);
        uiTextScore.gameObject.SetActive(state);
        uiTextWave.gameObject.SetActive(state);
    }

    private void SetShowHighScorePanel(bool state)
    {
        //uiPanelDeathMenu.SetActive(state);
        uiPanelDeathMenu.gameObject.SetActive(state);
    }

    public bool isShowingLevelDoneUI()
    {
        return uiPanelLevelDone.gameObject.activeSelf;
    }

    private void SetShowLevelDonePanel(bool state)
    {        
        uiPanelLevelDone.gameObject.SetActive(state);
    }

    void ShowLevelDonePanel()
    {                        
        SetShowLevelDonePanel(true);
        bool nextLevelExists = GetComponent<LevelManager>().nextLevelExists();
        buttonLevelDonePlayNext.gameObject.SetActive(nextLevelExists);

        float levelScore = GetComponent<CountScore>().GetLevelScore();
        uiTextLevelDoneLine1.text = "Score for this level: " + levelScore.ToString("n2");
        uiTextLevelDoneLine1.text = "Total score: ";

        if (nextLevelExists)
        {
            uiTextLevelDoneTitle.text = "Level Completed!";
            buttonLevelDoneToMainMenu.GetComponentInChildren<Text>().text = "Abort to Main menu";
        }
        else
        {
            uiTextLevelDoneTitle.text = "Game Completed!";
            buttonLevelDoneToMainMenu.GetComponentInChildren<Text>().text = "Back to Main menu";
        }
    }

    void HideLevelDonePanel()
    {
        SetShowLevelDonePanel(false);
    }

    void ShowHighScorePanel()
    {
        SetShowHighScorePanel(true);
    }

    void HideHighScorePanel()
    {
        SetShowHighScorePanel(false);
    }
}
