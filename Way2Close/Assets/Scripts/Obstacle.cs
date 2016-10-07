using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {        
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Player collided with obstacle.");
            GameObject player = coll.gameObject;
            player.GetComponent<PlayerDie>().KillPlayer();
        }
	
	}
		
}
