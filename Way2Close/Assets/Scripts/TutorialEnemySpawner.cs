using UnityEngine;
using System.Collections;

public class TutorialEnemySpawner : MonoBehaviour {



    public GameObject enemy;
    float objectRenderWidth;
    float objectRenderHeight;

    void Start()
    {
        objectRenderWidth = enemy.GetComponent<Renderer>().bounds.size.x;
        objectRenderHeight = enemy.GetComponent<Renderer>().bounds.size.y;
    }

    void Spawn()
    {
        Vector3 spawnPos = new Vector3();
        float randVal = Random.Range(0.0F, (Screen.width * 0.9F));

        spawnPos.x = Screen.width + objectRenderWidth + randVal;
        //Debug.Log("Spawning background enemy in main menu, adding random value " + randVal.ToString() + " to x coord. Resulting value is " + spawnPos.x.ToString() + ".");
        spawnPos.y = Random.Range(0 + objectRenderHeight, Screen.height - objectRenderHeight);
        spawnPos.z = 0.0F;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        worldPos.z = 0;

        Instantiate(enemy, worldPos, Quaternion.identity);
    }
}
