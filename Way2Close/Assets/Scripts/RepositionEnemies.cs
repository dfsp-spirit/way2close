using UnityEngine;
using System.Collections;

public class RepositionEnemies : MonoBehaviour {
			
	void OnTriggerEnter2D (Collider2D coll) {
        //Debug.Log("Trigger entered.");
	    if(coll.gameObject.tag == "Enemy")
        {
            GameObject enemy = coll.gameObject;
            float objectRenderHeight = enemy.GetComponent<Renderer>().bounds.size.y;
            float objectRenderWidth = enemy.GetComponent<Renderer>().bounds.size.x;

            //Vector3 oldPos = enemy.transform.position;
            

            Vector3 spawnPos = new Vector3();
            spawnPos.x = Screen.width + objectRenderWidth + Random.Range(0, 3 * objectRenderWidth);            
            spawnPos.y = Random.Range(0 - objectRenderHeight, Screen.height + objectRenderHeight);
            //spawnPos.x = 1.1F;
            //spawnPos.y = 1.0F;
            spawnPos.z = 0F;
            spawnPos = Camera.main.ScreenToWorldPoint(spawnPos);
            spawnPos.z = 0F;
            //spawnPos = Camera.main.ViewportToWorldPoint(spawnPos);

            enemy.transform.position = spawnPos;
            //Debug.Log("Trigger entered by enemy, moved enemy from " + oldPos.ToString() + " to " + spawnPos.ToString() + ".");
        }
	}
}
