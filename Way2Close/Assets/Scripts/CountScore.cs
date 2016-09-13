using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CountScore : MonoBehaviour {

    public Text scoreText;
    public Text multiplierText;
    float scoreOuterRadius = 5.0F;
    float scoreInnerRadius = 2.5F;
    float scorePerSecond = 1.0F;
    float score;
    int multiplier;
    int layerMaskEnemies = 1 << 9;
    LineRenderer lineRenderer;
    GameObject player;
    List<Vector3> myPoints;

    void Start () {
        score = 0;        
        //lineRenderer = Camera.main.GetComponent<LineRenderer>();
        //lineRenderer.enabled = false;
        player = GameObject.FindWithTag("Player");        

        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(Color.white, Color.white);
        lineRenderer.SetWidth(0.05F, 0.05F);
        lineRenderer.SetVertexCount(2);
        lineRenderer.sortingLayerName = "Front";
    }
	

	void Update () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        myPoints = new List<Vector3>();
        //float t = Time.time;
        //int i = 0;
        //while (i < lengthOfLineRenderer)
        //{
        //    Vector3 pos = new Vector3(i * 0.5F, Mathf.Sin(i + t), 0);
        //    lineRenderer.SetPosition(i, pos);
        //    i++;
        //}

        multiplier = 1;
        Vector3 playerCenter = player.GetComponent<Renderer>().bounds.center;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        string positionInfo = "";
        foreach (GameObject enemy in enemies)
        {
            positionInfo += " " + enemy.transform.position;
            //lineRenderer.enabled = true;            
            //lineRenderer.enabled = true;

            Vector3 distance = player.transform.position - enemy.transform.position;
            if(distance.sqrMagnitude < 5.0F)
            {
                multiplier++;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, player.transform.position);
                lineRenderer.SetPosition(1, enemy.transform.position);
                //Gizmos.DrawLine(player.transform.position, enemy.transform.position);
            }
            else
            {
                lineRenderer.enabled = false;
            }
            
        }

        Debug.Log("Player center at " + playerCenter.ToString() + ", " + (multiplier - 1).ToString() + " colliders in range.");
        //printEnemyPos();

        score += multiplier * (Time.deltaTime * scorePerSecond);
        scoreText.text = "Score: " + score.ToString("n2");
        multiplierText.text = multiplier.ToString() + "x";
        multiplierText.fontSize = 20 + (2 * multiplier);
        multiplierText.color = Color.white;
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
