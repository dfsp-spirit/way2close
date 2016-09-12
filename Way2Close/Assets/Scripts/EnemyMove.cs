using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public float speed;
    bool stop = false;
	
	// Update is called once per frame
	void Update () {
        if(!stop)
        {
            Vector3 vel = new Vector3();
            vel = Vector3.left * speed * Time.deltaTime;
            transform.Translate(vel);
        }
	
	}

    void StopMoving()
    {
        stop = true;
    }

    void StartMoving()
    {
        stop = false;
    }
}
