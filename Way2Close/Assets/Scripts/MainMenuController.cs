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

    public string[] getLevelSceneNames()
    {        
        return LevelManager.levelSceneNames.ToArray();
    }

    public void ShowMenuPanelLevelSelect()
    {
        panelMain.SetActive(false);
        panelLevelSelect.SetActive(true);
        panelAbout.SetActive(false);

        setUnlockedLevelsInLevelSelectPanel();
    }

    void setUnlockedLevelsInLevelSelectPanel()
    {
        bool[] levelUnlocked = getLevelUnlockedStatus();
        for(int i = 0; i < levelUnlocked.Length; i++)
        {
            setLevelButtonState(i, levelUnlocked[i]);
        }
    }

    void setLevelButtonState(int level, bool state)
    {
        Button b = getLevelSelectButtonForLevel(level);
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
    
    bool[] getLevelUnlockedStatus()
    {
        string[] levelNames = getLevelSceneNames();
        int numLevels = levelNames.Length;
        bool[] levelUnlocked = new bool[numLevels];
        for(int i = 0; i < numLevels; i++)
        {
            levelUnlocked[i] = false;
        }
        
        string keyName;
        for (int i = 0; i < numLevels; i++)
        {
            keyName = "unlockedLevel_" + levelNames[i];
            if (PlayerPrefs.HasKey(keyName))
            {
                levelUnlocked[i] = (PlayerPrefs.GetInt(keyName) == 1);
            }
        }

        if (numLevels > 0)
        {
            levelUnlocked[0] = true;    // first level is always available
        }

        return levelUnlocked;
    }

    public void unlockLevel(int levelIndex)
    {
        string[] levelNames = getLevelSceneNames();
        string keyName = "unlockedLevel_" + levelNames[levelIndex];
        PlayerPrefs.SetInt(keyName, 1);
    }
}
