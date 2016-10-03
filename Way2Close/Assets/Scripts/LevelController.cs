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
    public static float showNextPanelInTime = 8.0F;
    public static float uiFadeDuration = 1.0F;

    protected virtual void Start()
    {
        gameController = GameObject.Find("GameController");
        texts = levelPanel.GetComponentsInChildren<Text>();
        levelPanel.SetActive(false);
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


}
