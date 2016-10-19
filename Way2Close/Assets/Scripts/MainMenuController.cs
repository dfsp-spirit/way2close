using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class MainMenuController : MonoBehaviour {

    public GameObject panelMain;
    public GameObject panelLevelSelect;
    public GameObject panelAbout;
    public GameObject panelHighScores;

    void Start()
    {
        LeaderBoard.ResetScoreThisGame();
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_NONE_YET);

        // Comment out the next line to save highscores in release!
        //LeaderBoard.ResetAllHighScoresToZero();

        // Comment out the next two lines in release!
        //LevelManager.UnlockAllLevels();
        //LevelManager.LockAllLevels();

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

    public void ClickTrainingLevel2()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_TRAINING);
        SceneManager.LoadScene(LevelManager.sceneName_Level_2);
    }

    public void ClickTrainingLevel3()
    {
        LeaderBoard.SetGameModeThisGame(LeaderBoard.GAMEMODE_TRAINING);
        SceneManager.LoadScene(LevelManager.sceneName_Level_3);
    }

    public void ClickExit () {
        Application.Quit();
    }

    public void ClickLevelSelect()
    {
        ShowMenuPanelLevelSelect();
    }

    public void ClickHighScoresDone()
    {
        ShowMenuPanelMain();
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
        panelHighScores.SetActive(false);
    }

    public void ShowMenuPanelAbout()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(false);
        panelAbout.SetActive(true);
        panelHighScores.SetActive(false);
    }

    public void ShowMenuPanelHighScores()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(false);
        panelAbout.SetActive(false);
        panelHighScores.SetActive(true);

        PopulateHighScoresInfoPanel();
    }



    public void ShowMenuPanelLevelSelect()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(true);
        panelAbout.SetActive(false);
        panelHighScores.SetActive(false);

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

    void PopulateHighScoresInfoPanel()
    {
        Button highScoresTotal = GetTotalHighScoresInfoButton();
        highScoresTotal.GetComponentInChildren<Text>().text = GenerateTotalHighScoreText();

        Button highScoresPerLevel = GetPerLevelHighScoresInfoButton();
        highScoresPerLevel.GetComponentInChildren<Text>().text = GeneratePerLevelHighScoreText();
    }

    string GenerateTotalHighScoreText()
    {
        float score = LeaderBoard.GetGlobalHighscore();
        string date = LeaderBoard.GetGlobalHighscoreDateString();
        string fullDateInfo = "";
        string fullScoreInfo = score.ToString("n2") + " points";
        if (date != "")
        {
            fullDateInfo = " @ " + date;
        }
        if(score <= 0.1F)
        {
            fullScoreInfo = "none";
            fullDateInfo = "";
        }
        return fullScoreInfo + fullDateInfo;
    }

    string GeneratePerLevelHighScoreText()
    {
        string t = "";

        float score;
        string date;
        string levelSceneName;

        string[] levelSceneNames = LevelManager.getLevelSceneNames();
        for (int levelIndex = 0; levelIndex < levelSceneNames.Length; levelIndex++)
        {
            levelSceneName = levelSceneNames[levelIndex];
            score = LeaderBoard.GetHighscoreForLevelBySceneName(levelSceneName);
            date = LeaderBoard.GetHighscoreDateStringForLevelBySceneName(levelSceneName);
            string fullDateInfo = "";
            string fullScoreInfo = score.ToString("n2") + " points";
            if (date != "")
            {
                fullDateInfo = " @ " + date;
            }
            if (score <= 0.1F)
            {
                fullScoreInfo = "none";
                fullDateInfo = "";
            }
            t += ("Level " + levelIndex + ": " + fullScoreInfo + fullDateInfo + "\n");
        }
        return t;
    }

    Button GetTotalHighScoresInfoButton()
    {
        Button[] buttons = panelHighScores.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            if (b.name == "ButtonDisabledHighScoresTotal")
            {
                return b;
            }
        }
        return null;
    }

    Button GetPerLevelHighScoresInfoButton()
    {
        Button[] buttons = panelHighScores.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            if (b.name == "ButtonDisabledHighScoresPerLevel")
            {
                return b;
            }
        }
        return null;
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
