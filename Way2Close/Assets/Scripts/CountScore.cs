using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    void Start () {
        score = 0;
        multiplier = 1;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        player = GameObject.FindWithTag("Player");
    }
	

	void Update () {
        Vector3 playerCenter = player.GetComponent<Renderer>().bounds.center;

        Collider[] outerColliders = Physics.OverlapSphere(playerCenter, scoreOuterRadius);
        foreach(Collider enemy in outerColliders)
        {
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, enemy.transform.position);
            lineRenderer.enabled = true;
        }

        multiplier += outerColliders.Length;

        Debug.Log("Player center at " + playerCenter.ToString() + ", " + outerColliders.Length.ToString() + " colliders in range " + scoreOuterRadius.ToString() + ".");
        printEnemyPos();

        score += multiplier * (Time.deltaTime * scorePerSecond);
        scoreText.text = "Score: " + score.ToString("n2");
        multiplierText.text = multiplier.ToString() + "x";
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
            lineRenderer.SetPosition(0, new Vector3(0, 0));
            lineRenderer.SetPosition(1, new Vector3(500, 500));
            lineRenderer.enabled = true;
        }
        Debug.Log("Found " + enemies.Length.ToString() + " enemies:" + positionInfo);
    }
}
