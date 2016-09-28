using UnityEngine;
using System.Collections;

public class DestroyOutOfScreenEnemies : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Trigger entered.");
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject);
        }
	}
	
	
}
