using UnityEngine;
using System.Collections;

public class SetInitialPositions : MonoBehaviour {

    CameraRelativeViewportPositions cameraRelativeViewportPositionsScript;
    // Use this for initialization
    void Start () {

        Debug.Log("Setting initial positions...");
        cameraRelativeViewportPositionsScript = Camera.main.GetComponent<CameraRelativeViewportPositions>();
        SetPlayerStartPos();
        SetTriggerPos();
        Debug.Log("Initial positions set.");
    }

    void SetPlayerStartPos()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerScreenCoordSpawnPos = new Vector3();
        playerScreenCoordSpawnPos.x = cameraRelativeViewportPositionsScript.getScreenPlayerPosX();
        playerScreenCoordSpawnPos.y = cameraRelativeViewportPositionsScript.getScreenCoordForRelativeViewportPositionY(0.5F);
        playerScreenCoordSpawnPos.z = 0F;


        Vector3 worldPos = Camera.main.ScreenToWorldPoint(playerScreenCoordSpawnPos);
        worldPos.z = 0;

        player.transform.position = worldPos;
        Debug.Log("Set initial player position to " + player.transform.position.ToString());
    }

    void SetTriggerPos()
    {
        GameObject trigger = GameObject.Find("EnemyOutOfScreenTrigger");
        Vector3 triggerScreenCoordSpawnPos = new Vector3();
        triggerScreenCoordSpawnPos.x = cameraRelativeViewportPositionsScript.getScreenEnemyOutOfScreenTriggerPosX();
        triggerScreenCoordSpawnPos.y = cameraRelativeViewportPositionsScript.getScreenCoordForRelativeViewportPositionY(0.5F);
        triggerScreenCoordSpawnPos.z = 0F;


        Vector3 worldPos = Camera.main.ScreenToWorldPoint(triggerScreenCoordSpawnPos);
        worldPos.z = 0;

        trigger.transform.position = worldPos;
        Debug.Log("Set initial trigger position to " + trigger.transform.position.ToString());
    }
	
}
