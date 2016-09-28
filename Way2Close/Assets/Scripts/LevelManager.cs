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

    public static List<string> levelFancyNames = new List<string>() { "Randomness", "The Stairs" };
    public static List<string> levelSceneNames = new List<string>() { sceneName_Level_0, sceneName_Level_1 };
        

    public bool nextLevelExists()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentSceneIndex = levelSceneNames.IndexOf(currentSceneName);

        if (currentSceneIndex >= 0)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex >= 0 && nextSceneIndex < levelSceneNames.Count)
            {
                return true;
            }
        }

        return false;
    }

    public void ButtonNextLevelClicked()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentSceneIndex = levelSceneNames.IndexOf(currentSceneName);

        if(currentSceneIndex >= 0)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex >= 0 && nextSceneIndex < levelSceneNames.Count)
            {
                string nextLevelSceneName = levelSceneNames[nextSceneIndex];
                SceneManager.LoadScene(nextLevelSceneName);
            }
            
        }

        // fall back to main menu if something went wrong
        SceneManager.LoadScene(sceneName_MainMenu);
    }

    public string GetLevelNameBySceneName(string sceneName)
    {
        int sceneIndex = levelSceneNames.IndexOf(sceneName);
        if(sceneIndex >= 0)
        {
            return levelFancyNames[sceneIndex];
        }
        return null;
    }

    public string getMainMenuSceneName()
    {
        return sceneName_MainMenu;
    }
}
