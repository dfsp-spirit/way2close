using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {


    public Text timeLeftText;


    float timeSinceLevelLoaded;
    float maxLevelTime = 100F;
    float timeLeft;
    bool updateLevelTime;
    bool levelEnded;

  
    void Start () {
        updateLevelTime = true;
        timeLeft = maxLevelTime;
        levelEnded = false;
    }
	
	

    void StopUpdatingLevelTime()
    {
        updateLevelTime = false;
    }

    void Update()
    {
        UpdateTime();
        UpdateTimerGUI();
        CheckForLevelTimeEnded();
    }

    void UpdateTime()
    {
        if (updateLevelTime)
        {
            timeSinceLevelLoaded = Time.timeSinceLevelLoad;
            timeLeft = maxLevelTime - timeSinceLevelLoaded;
        }
    }

    void UpdateTimerGUI()
    {
        timeLeftText.text = "Time left: " + timeLeft.ToString("n2");
        timeLeftText.color = Color.white;
        if (timeLeft < (maxLevelTime * 0.5F))
        {
            timeLeftText.color = Color.yellow;
        }
        if (timeLeft < (maxLevelTime * 0.25F))
        {
            timeLeftText.color = Color.magenta;
        }
        if (timeLeft < (maxLevelTime * 0.1F))
        {
            timeLeftText.color = Color.red;
        }
    }

    void CheckForLevelTimeEnded()
    {
        if(timeLeft <= 0.0F && (! levelEnded))
        {
            EndLevel();            
        }
    }

    void EndLevel()
    {
        levelEnded = true;
        GetComponent<LevelUIController>().SendMessage("ShowLevelDonePanel");        
    }
}
