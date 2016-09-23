using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

    //public AudioClip playerDieClip;
    bool isDead = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision involving player.");
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Player collided with enemy.");
            KillMe();
            StopEnemies();
            GameObject gc = GameObject.FindGameObjectWithTag("GameController");
            gc.SendMessage("StopAddingScore");            
            gc.SendMessage("StopSpawning");
            gc.SendMessage("StopUpdatingLevelTime");
            gc.SendMessage("UpdateHighscoreText");
            gc.SendMessage("ShowHighScorePanel");
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
