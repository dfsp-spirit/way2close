using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static string sceneName_Level_0 = "Level_0";
    public static string sceneName_Level_1 = "Level_1";

    public static string sceneName_MainMenu = "MainMenu";
    public static string sceneName_Tutorial = "Tutorial";

    public static List<string> levelFancyNames = new List<string>() { "Randomness", "Stairs" };
    public static List<string> levelSceneNames = new List<string>() { sceneName_Level_0, sceneName_Level_1 };
        

    public bool nextLevelExists()
    {

        int currentLevelIndex = GetCurrentLevelIndex();
        if (currentLevelIndex >= 0)
        {
            int nextSceneIndex = currentLevelIndex + 1;
            if (nextSceneIndex >= 0 && nextSceneIndex < levelSceneNames.Count)
            {
                return true;
            }
        }

        return false;
    }

    private int GetCurrentLevelIndex()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return levelSceneNames.IndexOf(currentSceneName);
    }

    // called at level end, when player completed level successfully
    public void UnlockNextLevelIfAppropriate()
    {
        if(nextLevelExists())
        {
            if (LeaderBoard.GetGameModeThisGame() == LeaderBoard.GAMEMODE_GAME)
            {
                int currentLevelIndex = GetCurrentLevelIndex();

                if (currentLevelIndex >= 0)
                {
                    int nextLevelIndex = currentLevelIndex + 1;
                    UnlockLevelByLevelIndex(nextLevelIndex);
                }
            }
        }
    }

    public void ButtonNextLevelClicked()
    {        
        int currentLevelIndex = GetCurrentLevelIndex();

        if(currentLevelIndex >= 0)
        {
            int nextLevelIndex = currentLevelIndex + 1;
            if(nextLevelIndex >= 0 && nextLevelIndex < levelSceneNames.Count)
            {
                string nextLevelSceneName = levelSceneNames[nextLevelIndex];
                SceneManager.LoadScene(nextLevelSceneName);
            }
            
        }

        // fall back to main menu if something went wrong
        SceneManager.LoadScene(sceneName_MainMenu);
    }

    public static string GetLevelNameBySceneName(string sceneName)
    {
        int levelIndex = levelSceneNames.IndexOf(sceneName);
        if(levelIndex >= 0)
        {
            return levelFancyNames[levelIndex];
        }
        return null;
    }

    public static string getMainMenuSceneName()
    {
        return sceneName_MainMenu;
    }


    public static void UnlockLevelByLevelIndex(int levelIndex)
    {
        Debug.Log("LevelManager unlocked level with index " + levelIndex);
        string keyName = LevelManager.getLevelUnlockedKeyForLevelByLevelIndex(levelIndex);
        PlayerPrefs.SetInt(keyName, 1);
        PlayerPrefs.Save();
    }

    public static string[] getLevelSceneNames()
    {
        return LevelManager.levelSceneNames.ToArray();
    }

    public static string[] getLevelFancyNames()
    {
        return LevelManager.levelFancyNames.ToArray();
    }

    private static string getLevelUnlockedKeyForLevelByLevelIndex(int levelIndex)
    {
        string[] levelSceneNames = LevelManager.getLevelSceneNames();
        return "unlockedLevel_" + levelSceneNames[levelIndex];
    }

    public static bool isLevelUnlockedBySceneName(string sceneName)
    {
        int levelIndex = levelSceneNames.IndexOf(sceneName);
        bool[] unlocked = LevelManager.getAllLevelsUnlockedStatus();
        return unlocked[levelIndex];
    }

    public static bool[] getAllLevelsUnlockedStatus()
    {
        string[] levelSceneNames = LevelManager.getLevelSceneNames();
        int numLevels = levelSceneNames.Length;
        bool[] levelUnlocked = new bool[numLevels];
        for (int i = 0; i < numLevels; i++)
        {
            levelUnlocked[i] = false;
        }

        string keyName;
        for (int i = 0; i < numLevels; i++)
        {
            keyName = LevelManager.getLevelUnlockedKeyForLevelByLevelIndex(i);
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
}
