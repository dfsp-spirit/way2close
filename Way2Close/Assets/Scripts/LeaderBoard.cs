using UnityEngine;
using System.Collections;

public class LeaderBoard : MonoBehaviour {



    private static string highScoreTotalEverKey = "Highscore";
    private static string totalScoreThisGameKey = "ScoreThisGame";
    private static string gameModeThisGameKey = "GameMode";

    public static string GAMEMODE_TRAINING = "training";
    public static string GAMEMODE_GAME = "game";
    public static string GAMEMODE_TUTORIAL = "tutorial";
    public static string GAMEMODE_NONE_YET = "none";    // in menu

    public static float GetScoreThisGame()
    {
        if (PlayerPrefs.HasKey(LeaderBoard.totalScoreThisGameKey))
        {
            return PlayerPrefs.GetFloat(totalScoreThisGameKey);
        }
        else
        {
            return 0.0F;
        }
    }

    public static string GetGameModeThisGame()
    {
        if(PlayerPrefs.HasKey(gameModeThisGameKey))
        {
            return PlayerPrefs.GetString(gameModeThisGameKey);
        }
        else
        {
            return LeaderBoard.GAMEMODE_NONE_YET;
        }
    }

    public static void Report()
    {
        Debug.Log("Global highscore:\t" + LeaderBoard.GetGlobalHighscore().ToString("n2"));
        Debug.Log("Highscore by level:");
        string[] levelSceneNames = LevelManager.getLevelSceneNames();
        foreach (string sceneName in levelSceneNames)
        {
            Debug.Log("Level '" + sceneName + "':\t" + LeaderBoard.GetHighscoreForLevelBySceneName(sceneName).ToString("n2"));
        }
        Debug.Log("Level state (unlocked/locked) by level:");
        foreach (string sceneName in levelSceneNames)
        {
            Debug.Log("Level '" + sceneName + "':\t" + (LevelManager.isLevelUnlockedBySceneName(sceneName) ? "yes" : "no"));
        }
    }

    // gameMode: use on of the constants in LeaderBoard.GAMEMODE_*
    public static void SetGameModeThisGame(string gameMode)
    {
        PlayerPrefs.SetString(gameModeThisGameKey, gameMode);
    }

    public static void SetScoreThisGame(float score)
    {
        string key = LeaderBoard.totalScoreThisGameKey;
        PlayerPrefs.SetFloat(key, score);
        PlayerPrefs.Save();
    }

    public static void ResetScoreThisGame()
    {
        LeaderBoard.SetScoreThisGame(0.0F);
    }

    public static bool IsGlobalHighscore(float myScore)
    {
        return (myScore > LeaderBoard.GetGlobalHighscore());
    }

    public static bool IsLevelHighscoreBySceneName(string sceneName, float myScore)
    {
        return (myScore > LeaderBoard.GetHighscoreForLevelBySceneName(sceneName));
    }


    public static float GetGlobalHighscore()
    {        
        if (PlayerPrefs.HasKey(LeaderBoard.highScoreTotalEverKey))
        {
            return PlayerPrefs.GetFloat(highScoreTotalEverKey);
        }
        else
        {
            return 0.0F;
        }        
    }

    public static float GetHighscoreForLevelBySceneName(string sceneName)
    {
        string key = GetHighscoreKeyNameForScene(sceneName);
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            return 0.0F;
        }
    }

    public static void SetGlobalHighscore(float score)
    {
        string key = LeaderBoard.highScoreTotalEverKey;
        PlayerPrefs.SetFloat(key, score);
        PlayerPrefs.Save();
    }

    public static void SetHighscoreForLevelBySceneName(string sceneName, float score)
    {
        string key = GetHighscoreKeyNameForScene(sceneName);
        PlayerPrefs.SetFloat(key, score);
        PlayerPrefs.Save();
    }


    private static string GetHighscoreKeyNameForScene(string sceneName)
    {
        return "Highscore_" + sceneName;
    }
}
