using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CountScore : MonoBehaviour {

    public Text scoreText;
    public Text multiplierText;

    public GameObject uiPanel;
    public Text textDeadTitle;
    public Text textDeadLine1;

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
    int numInitialEnemies;
    SpawnEnemies spawnEnemiesScript;

    void Start () {
        score = 0;
        addScore = true;
        player = GameObject.FindWithTag("Player");
        spawnEnemiesScript = GetComponent<SpawnEnemies>();
        numInitialEnemies = spawnEnemiesScript.numInitialEnemies;
        lineRenderers = new List<LineRenderer>();

        for(int i = 0; i < numInitialEnemies; i++)
        {
            addLineRenderer(lineRenderers);
        }

        beginningHighscore = 0.0F;
        if(PlayerPrefs.HasKey("Highscore"))
        {
            beginningHighscore = PlayerPrefs.GetFloat("Highscore");
        }
        uiPanel.SetActive(false);
    }
	
    void addLineRenderer(List<LineRenderer> lineRenderers)
    {
        GameObject objToSpawn = new GameObject("LineRendererHolder" + lineRenderers.Count.ToString());
        lineRenderer = objToSpawn.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(Color.white, Color.white);
        lineRenderer.SetWidth(0.05F, 0.05F);
        lineRenderer.SetVertexCount(2);
        lineRenderer.sortingLayerName = "Front";
        lineRenderers.Add(lineRenderer);
    }


    void Update () {

        multiplier = 1;
        Vector3 playerCenter = player.GetComponent<Renderer>().bounds.center;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(lineRenderers.Count < enemies.Length)
        {
            int diff = enemies.Length - lineRenderers.Count;
            for(int i = 0; i < diff; i++)
            {
                addLineRenderer(lineRenderers);
            }
        }

        string positionInfo = "";
        for(int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i];
            positionInfo += " " + enemy.transform.position;
            //lineRenderer.enabled = true;            
            //lineRenderer.enabled = true;

            Vector3 distance = player.transform.position - enemy.transform.position;
            if(distance.sqrMagnitude < scoreOuterRadius)
            {
                multiplier++;
                lineRenderers[i].enabled = true;
                lineRenderers[i].SetPosition(0, player.transform.position);
                lineRenderers[i].SetPosition(1, enemy.transform.position);

                if (distance.sqrMagnitude < scoreInnerRadius)
                {
                    multiplier++;
                    lineRenderers[i].SetColors(Color.red, Color.red);
                }
                else
                {
                    lineRenderers[i].SetColors(Color.white, Color.white);
                }
                //Gizmos.DrawLine(player.transform.position, enemy.transform.position);
            }
            else
            {
                lineRenderers[i].enabled = false;
            }
            
        }

        //Debug.Log("Player center at " + playerCenter.ToString() + ", " + (multiplier - 1).ToString() + " colliders in range.");
        //printEnemyPos();

        scoreText.text = "Score: " + score.ToString("n2");
        multiplierText.text = "-";
        multiplierText.fontSize = 20;
        multiplierText.color = Color.white;
        if (addScore)
        {
            score += multiplier * (Time.deltaTime * scorePerSecond);            
            multiplierText.text = multiplier.ToString() + "x";
            multiplierText.fontSize = 20 + (2 * multiplier);
            if (multiplier >= 2)
            {
                multiplierText.color = Color.yellow;
            }
            if (multiplier >= 4)
            {
                multiplierText.color = Color.magenta;
            }
            if (multiplier >= 6)
            {
                multiplierText.color = Color.red;
            }
        }
    }

    void StopAddingScore()
    {
        addScore = false;
    }

    void ShowHighscore()
    {
        textDeadTitle.text = "YOU ARE DEAD";
        textDeadLine1.text = "Score: " + score.ToString("n2");
        if (score > beginningHighscore)
        {
            PlayerPrefs.SetFloat("Highscore", score);
            PlayerPrefs.Save();
            textDeadLine1.text = "New Highscore: " + score.ToString("n2");
        }
        uiPanel.SetActive(true);
    }

    void printEnemyPos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        string positionInfo = "";
        foreach(GameObject enemy in enemies)       
        {
            positionInfo += " " + enemy.transform.position;
            //lineRenderer.SetPosition(0, player.transform.position);
            //lineRenderer.SetPosition(1, enemy.transform.position);
            //lineRenderer.SetPosition(0, new Vector3(0, 0));
            //lineRenderer.SetPosition(1, new Vector3(500, 500));
            //lineRenderer.enabled = true;

            Vector3 distance = player.transform.position - enemy.transform.position;
            Debug.Log("Found enemy in distance " + distance.ToString() + ". Magnitude is " + distance.magnitude.ToString() + ". SqrMagnitude is " + distance.sqrMagnitude.ToString() + ".");
        }
        Debug.Log("Found " + enemies.Length.ToString() + " enemies:" + positionInfo);
    }
}
