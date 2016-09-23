using UnityEngine;
using System.Collections;

public class CameraRelativeViewportPositions : MonoBehaviour {

    // Note that this script must be run BEFORE others that ask it for positions

    //Camera camera;
    Rect viewPort;
    float aspect;

    // Use this for initialization
    void Start () {
        updateCameraAndScreenProps();
        //printScreenInfo();
    }

    void updateCameraAndScreenProps()
    {
        viewPort = GetComponent<Camera>().pixelRect;
        aspect = (float)Screen.width / (float)Screen.height;
    }

    // if the position should be on the screen, then x and y must be between 0 and 1
    public Vector3 getWorldCoordsForRelativeViewportPosition(float x, float y)
    {
        Vector3 v = new Vector3();

        v.x = getWorldCoordForRelativeViewportPositionX(x);
        v.y = getScreenCoordForRelativeViewportPositionY(y);
        v.z = 0F;

        return v;
    }

    public float getWorldCoordForRelativeViewportPositionX(float x)
    {
        return viewPort.xMin + (x * viewPort.width);
    }

    public float getScreenCoordForRelativeViewportPositionY(float y)
    {
        return viewPort.yMin + (y * viewPort.height);
    }


    void printScreenInfo()
    {
        Debug.Log("Screen width is " + Screen.width + ", height is " + Screen.height + ".");
        Debug.Log("Camera viewport width is " + viewPort.width + ", height is " + viewPort.height + ".");
        Debug.Log("Aspect ratio is: " + aspect.ToString("n2") + "(16:9=" + (16.0 / 9.0).ToString("n2") + ",5:4=" + ((5.0 / 4.0)).ToString("n2") + ").");
        Debug.Log("World coord of viewport position (0.0, 0.0)=" + getWorldCoordsForRelativeViewportPosition(0.0F, 0.0F).ToString() + ".");
        Debug.Log("World coord of viewport position(0.5, 0.5)=" + getWorldCoordsForRelativeViewportPosition(0.5F, 0.5F).ToString() + ".");
        Debug.Log("World coord of viewport position(1.0, 1.0)=" + getWorldCoordsForRelativeViewportPosition(1.0F, 1.0F).ToString() + ".");
        Debug.Log("Off-screen world coord of viewport position(-0.2, 0.5) which is slightly left of center of left viewport border =" + getWorldCoordsForRelativeViewportPosition(-0.2F, 0.5F).ToString() + ".");
        Debug.Log("Off-screen world coord of viewport position(1.2, 0.9) which is slightly right of upper right viewport corner =" + getWorldCoordsForRelativeViewportPosition(1.2F, 0.9F).ToString() + ".");
    }

    public float getScreenPlayerPosX()
    {
        return getWorldCoordForRelativeViewportPositionX(0.5F);
    }

    public float getScreenEnemyOutOfScreenTriggerPosX()
    {
        return getWorldCoordForRelativeViewportPositionX(-0.2F);
    }

    public float getWorldEnemySpawnPosX()
    {
        return getWorldCoordForRelativeViewportPositionX(1.1F);
    }
}
