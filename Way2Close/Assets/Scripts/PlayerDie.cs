using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

    public AudioClip playerDieClip;
    bool isDead = false;
		
	void KillMe () {
	    if(!isDead)
        {
            SendMessage("StopMoving");
            AudioSource.PlayClipAtPoint(playerDieClip, transform.position);
            isDead = true;
        }
	}
}
