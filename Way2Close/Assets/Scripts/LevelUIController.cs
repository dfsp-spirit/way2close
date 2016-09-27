using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour {

    public GameObject uiPanelDeathMenu;
    public Text uiTextMultiplier;
    public Text uiTextTime;
    public Text uiTextScore;
    public Text uiTextWave;

    // Use this for initialization
    void Start () {
        SetShowHighScorePanel(false);
        SetShowInGameHUD(true);
    }
		
    void ShowInGameHUD()
    {
        SetShowInGameHUD(true);
    }

    void HideInGameHUD()
    {
        SetShowInGameHUD(false);
    }

    void HideTimeAndWave()
    {
        uiTextTime.gameObject.SetActive(false);
        uiTextWave.gameObject.SetActive(false);
    }

    private void SetShowInGameHUD(bool state)
    {
        uiTextMultiplier.gameObject.SetActive(state);
        uiTextTime.gameObject.SetActive(state);
        uiTextScore.gameObject.SetActive(state);
        uiTextWave.gameObject.SetActive(state);
    }

    private void SetShowHighScorePanel(bool state)
    {
        //uiPanelDeathMenu.SetActive(state);
        uiPanelDeathMenu.gameObject.SetActive(state);
    }

    void ShowHighScorePanel()
    {
        SetShowHighScorePanel(true);
    }

    void HideHighScorePanel()
    {
        SetShowHighScorePanel(false);
    }
}
