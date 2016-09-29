using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject panelMain;
    public GameObject panelLevelSelect;
    public GameObject panelAbout;

    void Start()
    {
        LeaderBoard.ResetScoreThisGame();
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_NONE_YET);

        ShowMenuPanelMain();
    }

    public void ClickPlay()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_GAME);
        SceneManager.LoadScene(LevelManager.sceneName_Level_0);
    }

    public void ClickTutorial()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_TUTORIAL);
        SceneManager.LoadScene(LevelManager.sceneName_Tutorial);
    }

    public void ClickTrainingLevel0()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_TRAINING);
        SceneManager.LoadScene(LevelManager.sceneName_Level_0);
    }

    public void ClickTrainingLevel1()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_TRAINING);
        SceneManager.LoadScene(LevelManager.sceneName_Level_1);
    }

    public void ClickExit () {
        Application.Quit();
    }

    public void ClickLevelSelect()
    {
        ShowMenuPanelLevelSelect();
    }

    public void ClickAboutOk()
    {
        ShowMenuPanelMain();
    }

    public void ShowMenuPanelMain()
    {
        panelMain.SetActive(true);
        panelLevelSelect.SetActive(false);
        panelAbout.SetActive(false);
    }

    public void ShowMenuPanelAbout()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(false);
        panelAbout.SetActive(true);
    }

    

    public void ShowMenuPanelLevelSelect()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(true);
        panelAbout.SetActive(false);

        SetUnlockedLevelsInLevelSelectPanel();
        SetButtonLablesInLevelSelectPanel();
    }

    void SetButtonLablesInLevelSelectPanel()
    {
        string[] levelFancyNames = LevelManager.getLevelFancyNames();
        for(int levelIndex = 0; levelIndex < levelFancyNames.Length; levelIndex++)
        {
            Button b = getLevelSelectButtonForLevel(levelIndex);
            b.GetComponentInChildren<Text>().text = "Level " + levelIndex + ": " + levelFancyNames[levelIndex];
        }

        
    }

    void SetUnlockedLevelsInLevelSelectPanel()
    {
        bool[] levelUnlocked = LevelManager.getAllLevelsUnlockedStatus();
        for(int levelIndex = 0; levelIndex < levelUnlocked.Length; levelIndex++)
        {
            setLevelButtonState(levelIndex, levelUnlocked[levelIndex]);
        }
    }

    void setLevelButtonState(int levelIndex, bool state)
    {
        Button b = getLevelSelectButtonForLevel(levelIndex);
        if( b != null)
        {
            b.interactable = state;
        }
    }

    Button getLevelSelectButtonForLevel(int levelIndex)
    {
        Button[] buttons = panelLevelSelect.GetComponentsInChildren<Button>();
        foreach(Button b in buttons)
        {
            if(b.name == "ButtonLevelSelectLevel" + levelIndex.ToString())
            {
                return b;
            }
        }
        return null;
    }
    
    

    
}
