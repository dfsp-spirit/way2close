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

    // Use these to determine the spawn position of objects (you have to handle, i.e., add or remove, the render width though)
    protected float rightScreenBorderWorldPos;
    protected float leftScreenBorderWorldPos;
    protected float topScreenBorderWorldPos;
    protected float bottomScreenBorderWorldPos;

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

        rightScreenBorderWorldPos = this.getRightScreenBorderXWorldPos();
        leftScreenBorderWorldPos = this.getLeftScreenBorderXWorldPos();
        topScreenBorderWorldPos = this.getTopScreenBorderYWorldPos();
        bottomScreenBorderWorldPos = this.getBottomScreenBorderYWorldPos();

        Debug.Log("[LevelController] Determined screen borders in world coords: right=" + rightScreenBorderWorldPos + ", left=" + leftScreenBorderWorldPos + ", top=" + topScreenBorderWorldPos + ", bottom=" + bottomScreenBorderWorldPos + ".");
    }

    protected float getRightScreenBorderXWorldPos()
    {
        Vector3 spawnPos = new Vector3();
        spawnPos.x = Screen.width;
        spawnPos.y = 0.0F;
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        return worldPos.x;
    }

    protected float getLeftScreenBorderXWorldPos()
    {
        Vector3 spawnPos = new Vector3();

        float screenStartLeft = 0.0F;

        spawnPos.x = screenStartLeft;
        spawnPos.y = 0.0F;
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        return worldPos.x;
    }

    protected float getTopScreenBorderYWorldPos()
    {
        Vector3 spawnPos = new Vector3();
        spawnPos.x = 0.0F;
        spawnPos.y = Screen.height;
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        return worldPos.y;
    }

    protected float getBottomScreenBorderYWorldPos()
    {
        Vector3 spawnPos = new Vector3();

        float screenStartBottom = 0.0F;

        spawnPos.x = 0.0F;
        spawnPos.y = screenStartBottom;
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        return worldPos.y;
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
