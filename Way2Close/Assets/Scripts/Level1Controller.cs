using UnityEngine;
using System.Collections;

public class Level1Controller : LevelController {


    // Use this for initialization
    protected override void Start () {
        base.Start();
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
