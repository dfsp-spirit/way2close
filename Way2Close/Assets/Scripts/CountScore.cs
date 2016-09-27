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
    float score;
    float beginningHighscore;
    int multiplier;
    bool addScore;
    LineRenderer lineRenderer;
    List<LineRenderer> lineRenderers;
    GameObject player;
    GameObject gameController;
    //int numInitialEnemies;
    SpawnEnemies spawnEnemiesScript;

    void Start () {
        score = 0.0F;
        multiplier = 1;
        addScore = true;
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController");
        spawnEnemiesScript = GetComponent<SpawnEnemies>();
        //numInitialEnemies = spawnEnemiesScript.numInitialEnemies;
        lineRenderers = new List<LineRenderer>();

        //for(int i = 0; i < numInitialEnemies; i++)
        //{
        //    addLineRenderer(lineRenderers);
        //}

        beginningHighscore = 0.0F;
        if(PlayerPrefs.HasKey("Highscore"))
        {
            beginningHighscore = PlayerPrefs.GetFloat("Highscore");
        }

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            disableScoreUI();
            disableOnDeathUI();
        }
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
        
          return spawnEnemiesScript.currentWave;

    }

    void Update () {

        multiplier = 1 + getCurrentWave();
        //Debug.Log("Base muliplier is " + multiplier.ToString() + ".");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        UpdateLineRenderers(enemies);
        

        
        for(int i = 0; i < enemies.Length; i++)
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
        scoreText.text = "Score: " + score.ToString("n2");
        if (score > beginningHighscore)
        {
            scoreText.color = Color.red;
        }
        else
        {
            scoreText.color = Color.white;
        }

        if (addScore)
        {
            score += multiplier * (Time.deltaTime * scorePerSecond);
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
        textDeadLine1.text = "Score: " + score.ToString("n2");
        textDeadLine2.text = "Highscore: " + beginningHighscore.ToString("n2");
        if (score > beginningHighscore)
        {
            PlayerPrefs.SetFloat("Highscore", score);
            PlayerPrefs.Save();
            textDeadLine1.text = "New Highscore!";
            textDeadLine2.text = score.ToString("n2");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void ButtonToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
