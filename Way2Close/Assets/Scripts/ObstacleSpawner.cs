using UnityEngine;
using System.Collections;

public class ObstacleSpawner : PolygonSpawner {

    float obstacleSpeed = 2.0F;
    float upperBoarderYPos = 4.0F;
    float lowerBoarderYPos = -4.0F;

    public static Vector2[] verticesTrapez = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(2, 2),
            new Vector2(4, 2),
            new Vector2(6, 0)
        };

    public static Vector2[] verticesRectangle = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

    public static Vector2[] verticesTriangle = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(1, 0),
        };

    public float ObstacleSpeed
    {
        get
        {
            return obstacleSpeed;
        }

        set
        {
            obstacleSpeed = value;
        }
    }

    public float UpperBoarderYPos
    {
        get
        {
            return upperBoarderYPos;
        }

        set
        {
            upperBoarderYPos = value;
        }
    }

    public float LowerBoarderYPos
    {
        get
        {
            return lowerBoarderYPos;
        }

        set
        {
            lowerBoarderYPos = value;
        }
    }

    public GameObject SpawnObstaclePolygon(string name, Vector2[] vertices2D)
    {
        GameObject spawnedGameObject = SpawnPolygon(name, vertices2D);
        EnemyMove em = spawnedGameObject.AddComponent(typeof(EnemyMove)) as EnemyMove;
        em.speed = this.ObstacleSpeed;

        return spawnedGameObject;
    }

    // Spawn a polygon with 4 faces. you need to define only a single face by giving its start and end points. the rest is constructed automatically so that the poly fills the space below 
    // these points down to the lower border. The points upperLeft and upperRight must NOT be below the lower border, and the first one must be left of the right one.
    public GameObject SpawnPolyAtBottomBorderFromTo(string name, Vector2 upperLeft, Vector2 upperRight)
    {
        Vector2[] vertices2D = GetBottomPolyVerticesFromTo(upperLeft, upperRight);
        GameObject go = SpawnObstaclePolygon(name, vertices2D);
        Debug.Log("Spawned GameObject " + go.name + ", before alignment call. Position is " + go.transform.position.ToString() + ", vertices are " + v2ArrayToString(vertices2D) + ".");
        AlignGameObjectLowerBorderToYPosition(go, this.lowerBoarderYPos);
        return go;
    }

    private string v2ArrayToString(Vector2[] vertices2D)
    {
        string s = "verts: ";
        foreach(Vector2 v in vertices2D)
        {
            s += " " + v.ToString("n3");
        }
        return s;
    }

    private void AlignGameObjectLowerBorderToYPosition(GameObject go, float targetYPos)
    {
        float currentYMin = go.GetComponent<Renderer>().bounds.min.y;
        float currentYMax = go.GetComponent<Renderer>().bounds.max.y;
        float difference = targetYPos - currentYMin;

        //DEBUG
        if(difference < 0.1)
        {
            difference = -0.15F;
        }

        Vector3 currentPosition = go.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + difference, currentPosition.z);
        Debug.Log("AlignGameObjectLowerBorderToYPosition: Moving GameObject " + go.name + " from position " + currentPosition.ToString() + " to " + targetPosition.ToString() + ". Move distance is " + difference.ToString("n3") + ", initial y min pos was " + currentYMin.ToString("n3") + ", y max was " + currentYMax.ToString("n3") +", target y pos is " + targetYPos.ToString("n3") + ".");

        go.transform.position = targetPosition;

        float afterYMin = go.GetComponent<Renderer>().bounds.min.y;
        float afterYMax = go.GetComponent<Renderer>().bounds.max.y;

        Debug.Log("AlignGameObjectLowerBorderToYPosition: After move. New y min pos is " + afterYMin.ToString("n3") + ", y max is " + afterYMax.ToString("n3") + ".");
    }

    private Vector2[] GetBottomPolyVerticesFromTo(Vector2 upperLeft, Vector2 upperRight)
    {
        if(upperLeft.x >= upperRight.x)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon invalid. The left vertex is NOT left of the right one.");
        }
        if (upperLeft.y < this.lowerBoarderYPos || upperRight.y < this.lowerBoarderYPos)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon invalid. The two points must not be below the lower border.");
        }
        if (upperLeft.y == this.lowerBoarderYPos && upperRight.y == this.lowerBoarderYPos)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon invalid. The two points must not be both directly on the lower border.");
        }
        return new Vector2[] { new Vector2(upperLeft.x, this.lowerBoarderYPos), upperLeft, upperRight, new Vector2(upperRight.x, this.lowerBoarderYPos) };
    }

    public GameObject SpawnPolyAtBorderTopFromTo(string name, Vector2 lowerLeft, Vector2 lowerRight)
    {
        Vector2[] vertices2D = GetBottomPolyVerticesFromTo(lowerLeft, lowerRight);
        return SpawnObstaclePolygon(name, vertices2D);
    }

    private Vector2[] GetTopPolyVerticesFromTo(Vector2 lowerLeft, Vector2 lowerRight)
    {
        if (lowerLeft.x >= lowerRight.x)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon invalid. The left vertex is NOT left of the right one.");
        }
        if (lowerLeft.y > this.upperBoarderYPos || lowerRight.y > this.upperBoarderYPos)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon invalid. The two points must not be above the upper border.");
        }
        if (lowerLeft.y == this.upperBoarderYPos && lowerRight.y == this.upperBoarderYPos)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon invalid. The two points must not be both directly on the upper border.");
        }
        return new Vector2[] { new Vector2(lowerLeft.x, this.upperBoarderYPos), lowerLeft, lowerRight, new Vector2(lowerRight.x, this.upperBoarderYPos) };
    }
}
