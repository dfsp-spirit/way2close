using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraAspectRatioGizmos : MonoBehaviour
{

    /**
     * Display gizmo for different camera aspect ratios
     * @CreatedBy: Fredrick Bäcker
     * @Use: As you wish! ;)
     */

    //   3:2   4:3   16:10   5:3   17:10   16:9   5:4

    public bool layoutPortrait = true;
    public bool layoutLandscape = true;
    [SerializeField]
    public List<bool> portrait = new List<bool>();
    [SerializeField]
    public List<bool> landscape = new List<bool>();

    void OnDrawGizmos()
    {
        Camera c = GetComponent<Camera>();
        Gizmos.color = Color.blue;

        Matrix4x4 tmp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        float spread = c.farClipPlane - c.nearClipPlane;
        float center = (c.farClipPlane + c.nearClipPlane) * 0.5f;
        float size = c.orthographicSize * 2;

        if (layoutPortrait)
        {
            for (int i = 0; i < portrait.Count; i++)
            {
                if (portrait[i])
                {
                    // Show aspect ratio
                    float aspect = aspectPortrait(i);
                    Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(size * aspect, size, spread));
                }
            }
        }

        if (layoutLandscape)
        {
            for (int i = 0; i < landscape.Count; i++)
            {
                if (landscape[i])
                {
                    // Show aspect ratio
                    float aspect = aspectLandscape(i);
                    Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(size * aspect, size, spread));
                }
            }
        }
        Gizmos.matrix = tmp;

    }

    private float aspectPortrait(int index)
    {
        switch (index)
        {
            case 0:
                return 0.6666667f;
            case 1:
                return 0.75f;
            case 2:
                return 0.625f;
            case 3:
                return 0.6f;
            case 4:
                return 0.588235294f;
            case 5:
                return 0.5625f;
            //case 6:
             //   return 0.8f;
        }
        return 1;
    }

    private float aspectLandscape(int index)
    {
        switch (index)
        {
            case 0:
                return 1.5f;
            case 1:
                return 1.333333333f;
            case 2:
                return 1.6f;
            case 3:
                return 1.666666667f;
            case 4:
                return 1.7f;
            case 5:
                return 1.777777778f;
            //case 6:
              //  return 1.25f;
        }
        return 1;
    }
}