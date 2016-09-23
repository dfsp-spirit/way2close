using UnityEngine;
using System.Collections;

public class MainMenuSpawnBackgroundEnemies : MonoBehaviour {

    public GameObject enemy;
    float objectRenderWidth;
    float objectRenderHeight;
    // Use this for initialization
    void Start () {
        enemy.transform.localScale = new Vector3(0.25F, 0.25F, 1.0F);

        objectRenderWidth = enemy.GetComponent<Renderer>().bounds.size.x;
        objectRenderHeight = enemy.GetComponent<Renderer>().bounds.size.y;

        for (int i = 0; i < 3; i++)
        {
            Spawn();
        }

    }
	

    void Spawn()
    {
        
        //enemy.transform.localScale = new Vector3(0.5F, 0.5F, 1.0F);
        Vector3 spawnPos = new Vector3();
        spawnPos.x = Screen.width + objectRenderWidth + Random.Range(0, objectRenderWidth * 5);
        spawnPos.y = Random.Range(0 + objectRenderHeight, Screen.height - objectRenderHeight);
        spawnPos.z = 0;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;
        
        Instantiate(enemy, worldPos, Quaternion.identity);
    }
}
