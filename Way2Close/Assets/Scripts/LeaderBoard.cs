using UnityEngine;
using System.Collections;
using System;

public class LeaderBoard : MonoBehaviour {



    private static string highScoreTotalEverKey = "Highscore";
    private static string highScoreTotalEverDateStringKey = "HighscoreDate";
    private static string totalScoreThisGameKey = "ScoreThisGame";
    private static string gameModeThisGameKey = "GameMode";

    public static string GAMEMODE_TRAINING = "training";
    public static string GAMEMODE_GAME = "game";
    public static string GAMEMODE_TUTORIAL = "tutorial";
    public static string GAMEMODE_NONE_YET = "none";    // in menu

    public static string GetOurDateStringFormat(DateTime dt)
    {
        return String.Format("{0:u}", dt);
    }

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
            return PlayerPrefs.GetFloat(LeaderBoard.highScoreTotalEverKey);
        }
        else
        {
            return 0.0F;
        }        
    }

    public static string GetGlobalHighscoreDateString()
    {
        if (PlayerPrefs.HasKey(LeaderBoard.highScoreTotalEverDateStringKey))
        {
            return PlayerPrefs.GetString(LeaderBoard.highScoreTotalEverDateStringKey);
        }
        else
        {
            return "";
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

    public static string GetHighscoreDateStringForLevelBySceneName(string sceneName)
    {
        string key = GetHighscoreDateKeyNameForScene(sceneName);
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            return "";
        }
    }

    public static void SetGlobalHighscore(float score, DateTime dateTime)
    {
        string scoreKey = LeaderBoard.highScoreTotalEverKey;
        PlayerPrefs.SetFloat(scoreKey, score);

        string dateKey = LeaderBoard.highScoreTotalEverDateStringKey;
        PlayerPrefs.SetString(dateKey, LeaderBoard.GetOurDateStringFormat(dateTime));

        PlayerPrefs.Save();
    }

    public static void SetHighscoreForLevelBySceneName(string sceneName, float score, DateTime dateTime)
    {
        string key = GetHighscoreKeyNameForScene(sceneName);
        PlayerPrefs.SetFloat(key, score);

        string dateKey = GetHighscoreDateKeyNameForScene(sceneName);
        PlayerPrefs.SetString(dateKey, LeaderBoard.GetOurDateStringFormat(dateTime));

        PlayerPrefs.Save();
    }


    private static string GetHighscoreKeyNameForScene(string sceneName)
    {
        return "Highscore_" + sceneName;
    }

    private static string GetHighscoreDateKeyNameForScene(string sceneName)
    {
        return "HighscoreDate_" + sceneName;
    }
}
