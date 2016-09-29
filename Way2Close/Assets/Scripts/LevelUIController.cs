using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        // only show next next button in game mode (if such a level exists), not in training or tutorial modes
        buttonLevelDonePlayNext.gameObject.SetActive(false);    
        if (LeaderBoard.GetGameModeThisGame() == LeaderBoard.GAMEMODE_GAME)
        {
            buttonLevelDonePlayNext.gameObject.SetActive(nextLevelExists);
        }
            

        float levelScore = GetComponent<CountScore>().GetLevelScore();
        float gameScore = GetComponent<CountScore>().GetGameScore();
        uiTextLevelDoneLine1.text = "Level score: " + levelScore.ToString("n2");
        uiTextLevelDoneLine2.text = "Total score: " + gameScore.ToString("n2");

        if(SceneManager.GetActiveScene().name == LevelManager.sceneName_Tutorial || LeaderBoard.GetGameModeThisGame() == LeaderBoard.GAMEMODE_TUTORIAL)
        {
            buttonLevelDonePlayNext.gameObject.SetActive(false);
            uiTextLevelDoneTitle.text = "Tutorial completed.";
            uiTextLevelDoneLine2.text = "";
            buttonLevelDoneToMainMenu.GetComponentInChildren<Text>().text = "Back to Main menu";
        }

        if (LeaderBoard.GetGameModeThisGame() == LeaderBoard.GAMEMODE_GAME)
        {
            if (nextLevelExists)
            {
                uiTextLevelDoneTitle.text = "Level completed!";
                buttonLevelDoneToMainMenu.GetComponentInChildren<Text>().text = "Abort to Main menu";
            }
            else
            {
                uiTextLevelDoneTitle.text = "Game completed!";
                buttonLevelDoneToMainMenu.GetComponentInChildren<Text>().text = "Back to Main menu";
            }
        }

        if (LeaderBoard.GetGameModeThisGame() == LeaderBoard.GAMEMODE_TRAINING)
        {
            uiTextLevelDoneLine2.text = "";
            uiTextLevelDoneTitle.text = "Training level completed!";
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
