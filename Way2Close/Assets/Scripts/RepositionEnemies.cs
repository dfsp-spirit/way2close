using UnityEngine;
using System.Collections;

public class RepositionEnemies : MonoBehaviour {
			
	void OnTriggerEnter2D (Collider2D coll) {
        Debug.Log("Trigger entered.");
	    if(coll.gameObject.tag == "Enemy")
        {
            int objectRenderHeight = 150;
            int objectRenderWidth = 150;
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Screen.width + objectRenderWidth + Random.Range(0, objectRenderWidth * 3);
            spawnPos.y = Random.Range(0 + objectRenderHeight, Screen.height - objectRenderHeight);
            spawnPos.z = 0;

            coll.gameObject.transform.position = spawnPos;
            Debug.Log("Trigger entered by enemy, moved enemy.");
        }
	}
}
