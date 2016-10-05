using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CountScore : MonoBehaviour {

    public Text scoreText;
    public Text multiplierText;
    public Material lineRendererMaterialOuterRadius;
    public Material lineRendererMaterialInnerRadius;

    public GameObject uiPanelOnDeath;
    public Text textDeadTitle;
    public Text textDeadLine1;
    public Text textDeadLine2;

    float scoreOuterRadius = 5.0F;
    float scoreInnerRadius = 2.5F;
    float scorePerSecond = 1.0F;
    float levelScore;
    float gameScore;
    float beginningHighscoreThisLevel;
    float beginningHighScoreTotalEver;
    int multiplier;
    bool addScore;
    LineRenderer lineRenderer;
    List<LineRenderer> lineRenderers;
    GameObject player;
    //int numInitialEnemies;
    SpawnEnemies spawnEnemiesScript;

    void Start () {
        levelScore = 0.0F;
        multiplier = 1;
        addScore = true;
        player = GameObject.Find("Player");
        spawnEnemiesScript = GetComponent<SpawnEnemies>();
        
        lineRenderers = new List<LineRenderer>();
       

        initHighScores();

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            disableScoreUI();
            disableOnDeathUI();
        }
    }

    
    

    void initHighScores()
    {
        gameScore = LeaderBoard.GetScoreThisGame();
        beginningHighscoreThisLevel = LeaderBoard.GetHighscoreForLevelBySceneName(SceneManager.GetActiveScene().name);        
        beginningHighScoreTotalEver = LeaderBoard.GetGlobalHighscore();       
    }

    void disableOnDeathUI()
    {
        uiPanelOnDeath.gameObject.SetActive(false);
    }

    void disableScoreUI()
    {
        scoreText.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
    }
	
    void addLineRenderer(List<LineRenderer> lineRenderers)
    {
        GameObject objToSpawn = new GameObject("LineRendererHolder" + lineRenderers.Count.ToString());
        lineRenderer = objToSpawn.AddComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterialOuterRadius;
        lineRenderer.SetColors(Color.white, Color.white);
        lineRenderer.SetWidth(0.05F, 0.05F);
        lineRenderer.SetVertexCount(2);
        lineRenderer.sortingLayerName = "Front";
        lineRenderers.Add(lineRenderer);
    }


    int getCurrentWave()
    {        
          return spawnEnemiesScript.GetCurrentWave();
    }

    public float GetLevelScore()
    {
        return levelScore;
    }

    public float GetGameScore()
    {
        return gameScore;
    }

    void Update () {

        multiplier = 1 + getCurrentWave();
        //Debug.Log("Base muliplier is " + multiplier.ToString() + ".");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        UpdateLineRenderers(enemies);


        // switch all line renderers off by default. prevents some from showing which are not checked below anymore because the associated enemy was destroyed.
        for(int i = 0; i < lineRenderers.Count; i++)
        {
            lineRenderers[i].enabled = false;
        }


        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i];
            
        
            Vector3 distance = player.transform.position - enemy.transform.position;
            if(distance.sqrMagnitude < scoreOuterRadius)
            {
                multiplier++;                
                lineRenderers[i].SetPosition(0, player.GetComponent<Renderer>().bounds.center);
                lineRenderers[i].SetPosition(1, enemy.GetComponent<Renderer>().bounds.center);

                if (distance.sqrMagnitude < scoreInnerRadius)
                {
                    multiplier++;
                    lineRenderers[i].SetColors(Color.red, Color.red);
                    lineRenderers[i].material = lineRendererMaterialInnerRadius;
                }
                else
                {
                    lineRenderers[i].SetColors(Color.white, Color.white);
                    lineRenderers[i].material = lineRendererMaterialOuterRadius;
                }
                lineRenderers[i].enabled = true;
                //Gizmos.DrawLine(player.transform.position, enemy.transform.position);
            }
            else
            {
                lineRenderers[i].enabled = false;
            }
            
        }

        updateUIScoreMulti();
        
    }


    private void UpdateLineRenderers(GameObject[] enemies)
    {
        if (lineRenderers.Count < enemies.Length)
        {
            int diff = enemies.Length - lineRenderers.Count;
            for (int i = 0; i < diff; i++)
            {
                addLineRenderer(lineRenderers);
            }
        }
    }

    private void updateUIScoreMulti()
    {
        scoreText.text = "Score: " + levelScore.ToString("n2");
        if (levelScore > beginningHighscoreThisLevel)
        {
            scoreText.color = Color.red;
        }
        else
        {
            scoreText.color = Color.white;
        }

        if (addScore)
        {
            float addedScore = multiplier * (Time.deltaTime * scorePerSecond);
            levelScore += addedScore;
            gameScore += addedScore;
        }


        multiplierText.text = multiplier.ToString() + "x";
        multiplierText.fontSize = 20 + (2 * multiplier);
        multiplierText.fontSize = Mathf.Min(multiplierText.fontSize, 40);
        multiplierText.color = Color.white;
        if (multiplier >= 2)
        {
            multiplierText.color = Color.green;
        }
        if (multiplier >= 4)
        {
            multiplierText.color = Color.yellow;
        }
        if (multiplier >= 6)
        {
            multiplierText.color = Color.magenta;
        }
        if (multiplier >= 8)
        {
            multiplierText.color = Color.red;
        }
    }

    void StopAddingScore()
    {
        addScore = false;
    }

    void UpdateHighscoreText()
    {
        textDeadTitle.text = "YOU ARE DEAD";
        textDeadLine1.text = "Your score for this level: " + levelScore.ToString("n2");
        textDeadLine2.text = "Highscore for this level: " + beginningHighscoreThisLevel.ToString("n2");
        bool levelHighscore = false;
        bool gameHighscore = false;
        if (levelScore > beginningHighscoreThisLevel)
        {
            levelHighscore = true;
            LeaderBoard.SetHighscoreForLevelBySceneName(SceneManager.GetActiveScene().name, levelScore);
            textDeadLine1.text = "New Highscore for level!";
            textDeadLine2.text = levelScore.ToString("n2");
        }        
        if(gameScore > beginningHighScoreTotalEver)
        {
            gameHighscore = true;
            LeaderBoard.SetGlobalHighscore(gameScore);
            textDeadLine1.text = "New total Highscore!";
            textDeadLine2.text = gameScore.ToString("n2");
        }
        
        if(levelHighscore && gameHighscore)
        {
            textDeadLine1.text = "New level and total Highscores!";
            textDeadLine2.text = "Level: " + levelScore.ToString("n2")  + " Total: " + gameScore.ToString("n2");
        }
    }

    private void printEnemyPos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        string positionInfo = "";
        foreach(GameObject enemy in enemies)       
        {
            positionInfo += " " + enemy.transform.position;
            Vector3 distance = player.transform.position - enemy.transform.position;
            Debug.Log("Found enemy in distance " + distance.ToString() + ". Magnitude is " + distance.magnitude.ToString() + ". SqrMagnitude is " + distance.sqrMagnitude.ToString() + ".");
        }
        Debug.Log("Found " + enemies.Length.ToString() + " enemies:" + positionInfo);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ButtonRestartLevelClicked()
    {
        LeaderBoard.ResetScoreThisGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void ButtonToMainMenuClicked()
    {
        LeaderBoard.ResetScoreThisGame();
        SceneManager.LoadScene(LevelManager.sceneName_MainMenu);
    }
}
