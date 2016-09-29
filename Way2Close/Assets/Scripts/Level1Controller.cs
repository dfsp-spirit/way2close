using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {

    GameObject gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
        gameController.GetComponent<SpawnEnemies>().Spawn();
    }

    override public float GetLevelDuration()
    {
        return 100.0F;
    }

    override public bool GetLevelHasFixedDuration()
    {
        return true;
    }


}
