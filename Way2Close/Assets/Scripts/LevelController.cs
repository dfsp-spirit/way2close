using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class LevelController : MonoBehaviour {

    public abstract bool GetLevelHasFixedDuration();
    public abstract float GetLevelDuration();

    public GameObject levelPanel;
    public Text levelTextHeading;
    public Text levelTextLine;
    protected Text[] texts;
    protected GameObject gameController;

    public static float showPanelDuration = 5.0F;
    public static float showNextPanelInTime = 7.0F;
    public static float uiFadeDuration = 1.0F;

    protected ObstacleSpawner obstacleSpawner;

    protected virtual void Start()
    {
        gameController = GameObject.Find("GameController");
        texts = levelPanel.GetComponentsInChildren<Text>();
        levelPanel.SetActive(false);
        obstacleSpawner = new ObstacleSpawner();
        obstacleSpawner.SetMaterialByResourceName("ObstacleMaterialRed");
        obstacleSpawner.ResultingGameObjectColliderType = PolygonSpawner.ColliderType.Polygon2D;
        obstacleSpawner.ResultingGameObjectSortingLayerName = "Front";
        obstacleSpawner.ResultingGameObjectTag = "Obstacle";
    }

    protected void ShowPanel()
    {
        levelPanel.SetActive(true);

        foreach (Text t in texts)
        {
            t.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
            t.CrossFadeAlpha(1f, uiFadeDuration, false);
        }
    }

    protected void HidePanel()
    {
        foreach (Text t in texts)
        {
            t.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            t.CrossFadeAlpha(0.1f, uiFadeDuration, false);
        }


        Invoke("DeactivatePanel", uiFadeDuration);
    }

    protected void DeactivatePanel()
    {
        levelPanel.SetActive(false);
    }

    protected void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    protected void StopSpawning()
    {
        CancelInvoke();
    }

    // you can do stuff like freeze the moving level parts or cancel spawn waves here if needed
    protected abstract void SetLevelEndedLevelControllerMode();

    protected abstract int GetCurrentLevelIndex();

    protected void EndLevel()
    {
        gameController.GetComponent<LevelTimer>().EndLevel();
    }

    protected string GetLevelFancyName()
    {
        return LevelManager.GetLevelFancyNameByLevelIndex(GetCurrentLevelIndex());
    }


}
