﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {


    public Text timeLeftText;
    public LevelController levelController;

    float timeSinceLevelLoaded;
    float levelDuration;
    bool levelHasFixedDuration;
    float timeLeft;
    bool updateLevelTime;
    bool levelEnded;

  
    void Start () {

        levelDuration = levelController.GetLevelDuration();
        levelHasFixedDuration = levelController.GetLevelHasFixedDuration();

        if( ! levelHasFixedDuration)
        {
            timeLeftText.text = "";
        }

        updateLevelTime = true;
        timeLeft = levelDuration;
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
        if (levelHasFixedDuration)
        {
            UpdateTimerGUI();
            CheckForLevelTimeEnded();
        }
    }

    void UpdateTime()
    {
        if (updateLevelTime)
        {
            timeSinceLevelLoaded = Time.timeSinceLevelLoad;
            timeLeft = levelDuration - timeSinceLevelLoaded;
        }
    }

    void UpdateTimerGUI()
    {
        timeLeftText.text = "Time left: " + timeLeft.ToString("n2");
        timeLeftText.color = Color.white;
        if (timeLeft < (levelDuration * 0.5F))
        {
            timeLeftText.color = Color.yellow;
        }
        if (timeLeft < (levelDuration * 0.25F))
        {
            timeLeftText.color = Color.magenta;
        }
        if (timeLeft < (levelDuration * 0.1F))
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

        GameObject levelContollerHolder = GameObject.Find("LevelControllerHolder");
        if(levelContollerHolder != null)
        {
            levelContollerHolder.GetComponent<LevelController>().SendMessage("SetLevelEndedLevelControllerMode");
        }
    }
}
