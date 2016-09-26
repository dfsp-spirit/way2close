using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

    //public AudioClip playerDieClip;
    bool isDead = false;
    bool isInvincible = false;
    GameObject gameController;
    GameObject player;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        player = GameObject.Find("Player");
        Spawn();
    }

    void Spawn()
    {
        SetSpawnProtectionOnFor(6.0F);
        SetPlayerNoMoveFor(3.0F);
        //player.transform.position = new Vector3();
    }

    void SetPlayerNoMoveFor(float time)
    {
        player.SendMessage("StopMoving");
        Invoke("AllowPlayerMove", time);
    }

    void SetSpawnProtectionOnFor(float time)
    {
        this.isInvincible = true;
        Invoke("SetSpawnProtectionOff", time);
    }

    void AllowPlayerMove()
    {
        player.SendMessage("AllowMoving");
    }

    void SetSpawnProtectionOff()
    {
        this.isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision involving player.");
        if (other.gameObject.tag == "Enemy")
        {
            if ( ! this.isInvincible)
            {
                //Debug.Log("Player collided with enemy.");
                KillMe();
                StopEnemies();
                gameController.SendMessage("StopAddingScore");
                gameController.SendMessage("StopSpawning");
                gameController.SendMessage("StopUpdatingLevelTime");
                gameController.SendMessage("UpdateHighscoreText");
                gameController.SendMessage("ShowHighScorePanel");
            }
        }            
    }

    void KillMe () {
	    if(!isDead)
        {
            SendMessage("StopMoving");
            //AudioSource.PlayClipAtPoint(playerDieClip, transform.position);
            isDead = true;
        }
	}

    void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.SendMessage("StopMoving");
        }
    }
}
