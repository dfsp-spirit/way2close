using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    List<string> levelNames = new List<string>() { "Randomness" };
    List<string> levelSceneNames = new List<string>() { "Level_0" };
    string sceneNameMainMenu = "MainMenu";

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
        SceneManager.LoadScene(sceneNameMainMenu);
    }

    public string GetLevelNameBySceneName(string sceneName)
    {
        int sceneIndex = levelSceneNames.IndexOf(sceneName);
        if(sceneIndex >= 0)
        {
            return levelNames[sceneIndex];
        }
        return null;
    }

    public string getMainMenuSceneName()
    {
        return this.sceneNameMainMenu;
    }
}
