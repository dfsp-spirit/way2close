using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {


    public Text timeLeftText;


    float timeSinceLevelLoaded;
    float maxLevelTime = 10F;
    float timeLeft;
    bool updateLevelTime;
    bool levelEnded;

  
    void Start () {
        updateLevelTime = true;
        timeLeft = maxLevelTime;
        levelEnded = false;

        LeaderBoard.Report();
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
        if(timeLeft <= 0.0F)
        {            
            if ( ! levelEnded)
            {
                EndLevel();
            }
        }
        
    }

    // called when the player reached the end of the level successfully. NOT called on player death.
    void EndLevel()
    {
        levelEnded = true;
        GetComponent<CountScore>().SendMessage("StopAddingScore");
        GetComponent<SpawnEnemies>().SendMessage("StopSpawning");
        GetComponent<LevelTimer>().SendMessage("StopUpdatingLevelTime");
        timeLeft = 0.0F;    // prevent display of a slighty negative time at level end, like "-0.01 secsonds left"
        GetComponent<LevelUIController>().SendMessage("ShowLevelDonePanel");
        GetComponent<LevelUIController>().SendMessage("SaveScores");
        GetComponent<LevelManager>().SendMessage("UnlockNextLevelIfAppropriate");        
        GameObject player = GameObject.Find("Player");
        player.SendMessage("SetLevelEndedPlayerMode");     
    }
}
