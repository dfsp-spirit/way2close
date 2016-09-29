using UnityEngine;
using System.Collections;

public class Level1Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpawnEnemies>().Spawn();
    }
	
	
}
