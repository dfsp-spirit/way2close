using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    bool doInputChecking = true;
    bool playerThrust = false;
    float currentSpeed;
    public float maxSpeed; // positive y axis speed, i.e., max speed at which player moves up
    public float minSpeed; // negative y axis speed, i.e., max speed at which player moves down
    public float speedChangePerTime;

    Vector3 bottomLeft;
    Vector3 topRight;
    Rect cameraRect;


    void Start()
    {
        currentSpeed = 0.0F;
        bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

	// Update is called once per frame
	void Update () {
        // check for user input
        if(Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            currentSpeed += speedChangePerTime * Time.deltaTime;
            if(currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            playerThrust = true;
        }
        else
        {
            currentSpeed -= speedChangePerTime * Time.deltaTime;
            if (currentSpeed < minSpeed)
            {
                currentSpeed = minSpeed;
            }
            playerThrust = false;
        }


        Vector3 vel = new Vector3();
        vel = Vector3.up * currentSpeed * Time.deltaTime;

        transform.Translate(vel);

        // prevent player from leaving screen
        if (transform.position.y < cameraRect.yMin || transform.position.y > cameraRect.yMax)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax), Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), transform.position.z);
            currentSpeed = 0.0F;
        }
        
    }
}
