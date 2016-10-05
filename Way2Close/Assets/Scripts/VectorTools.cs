using UnityEngine;
using System.Collections;

public class VectorTools : MonoBehaviour {

	public static Vector3 PosAbove(Vector3 inPosition, float distance)
    {
        return new Vector3(inPosition.x, inPosition.y + distance, inPosition.z);
    }

    public static Vector3 PosRight(Vector3 inPosition, float distance)
    {
        return new Vector3(inPosition.x + distance, inPosition.y, inPosition.z);
    }

    public static Vector3 PosLeft(Vector3 inPosition, float distance)
    {
        return new Vector3(inPosition.x - distance, inPosition.y, inPosition.z);
    }

    public static Vector3 PosBelow(Vector3 inPosition, float distance)
    {
        return new Vector3(inPosition.x, inPosition.y - distance, inPosition.z);
    }
}
