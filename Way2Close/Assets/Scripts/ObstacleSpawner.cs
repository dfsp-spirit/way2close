using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : PolygonSpawner {

    float obstacleSpeed = 2.0F;
    float upperBoarderYPos = 4.15F;
    float lowerBoarderYPos = -4.15F;
    bool warnIfPolyOutofGameArea = true;


    public enum DeleteVertexToMakeTriangleInsteadOf4FacePoly { KeepAll4Vertices, DeleteIndex1ToCreateStartRampTriangle, DeleteIndex3ToCreateEndRampTriangle };

    public static Vector2[] verticesTrapez = new Vector2[] {
            new Vector2(0.0F, 0.0F),
            new Vector2(2.0F, 2.0F),
            new Vector2(4.0F, 2.0F),
            new Vector2(6.0F, 0.0F)
        };

    public static Vector2[] verticesRectangle = new Vector2[] {
            new Vector2(0.0F, 0.0F),
            new Vector2(0.0F, 1.0F),
            new Vector2(1.0F, 1.0F),
            new Vector2(1.0F, 0.0F)
        };

    public static Vector2[] verticesTriangle = new Vector2[] {
            new Vector2(0.0F, 0.0F),
            new Vector2(1.0F, 1.0F),
            new Vector2(1.0F, 0.0F),
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

    // can delete one of the vertes to create a 3-face poly (triangle) based on the 4 input vertices which define a 4-face poly
    private Vector2[] filterVerticesUsingDeleteOption(Vector2[] vertices2D, DeleteVertexToMakeTriangleInsteadOf4FacePoly deleteVertexOption)
    {
        
        if (deleteVertexOption == DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex1ToCreateStartRampTriangle)
        {
            vertices2D = vertices2D.RemoveAt(1);
        }
        if (deleteVertexOption == DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex3ToCreateEndRampTriangle)
        {
            vertices2D = vertices2D.RemoveAt(3);
        }        
        return vertices2D;
    }

    // Spawn a polygon with 4 faces. you need to define only a single face by giving its start and end points. the rest is constructed automatically so that the poly fills the space below 
    // these points down to the lower border. The points upperLeft and upperRight must NOT be below the lower border, and the first one must be left of the right one.
    public GameObject Spawn4FacePolyAtBottomBorderFromTo(string name, Vector2 upperLeft, Vector2 upperRight, DeleteVertexToMakeTriangleInsteadOf4FacePoly deleteVertexOption = DeleteVertexToMakeTriangleInsteadOf4FacePoly.KeepAll4Vertices)
    {
        Vector2[] vertices2D = GetBottomPolyVerticesFromTo(upperLeft, upperRight, name);
        vertices2D = filterVerticesUsingDeleteOption(vertices2D, deleteVertexOption);

        GameObject go = SpawnObstaclePolygon(name, vertices2D);
        //Debug.Log("Spawned GameObject " + go.name + ", before alignment call. Position is " + go.transform.position.ToString() + ", vertices are " + v2ArrayToString(vertices2D) + ".");
        //AlignGameObjectLowerBorderToYPosition(go, this.lowerBoarderYPos);
        return go;
    }

    public GameObject SpawnTopBorderObstacleParallelToPolyAtBottom(string name, Vector2 upperLeftOfBottomObstacle, Vector2 upperRightOfBottomObstacle, float verticalSpaceInBetween)
    {
        Vector2 lowerLeft = new Vector2(upperLeftOfBottomObstacle.x, upperLeftOfBottomObstacle.y + verticalSpaceInBetween);
        Vector2 lowerRight = new Vector2(upperRightOfBottomObstacle.x, upperRightOfBottomObstacle.y + verticalSpaceInBetween);
        return Spawn4FacePolyAtTopBorderFromTo(name, lowerLeft, lowerRight);
    }

    // give it the 2 points defining the upper edge of the cave floor and the height of the tunnel the player can fly through
    public GameObject[] SpawnTunnelSegmentDefinedByBottom(Vector2 upperLeft, Vector2 upperRight, float tunnelHeight)
    {
        string tunnelFloorName = "TunnelFloor";
        string tunnelCeilingName = "TunnelCeiling";
        // check only
        if (warnIfPolyOutofGameArea)
        {
            if(upperLeft.y < this.lowerBoarderYPos || upperRight.y < this.lowerBoarderYPos)
            {
                Debug.Log("SpawnTunnelSegmentDefinedByBottom: WARNING: Tunnel segment " + tunnelFloorName + " will be below lower border.");
            }

            if (upperLeft.y + tunnelHeight > this.upperBoarderYPos || upperRight.y + tunnelHeight > this.upperBoarderYPos)
            {
                Debug.Log("SpawnTunnelSegmentDefinedByBottom: WARNING: Tunnel segment " + tunnelCeilingName + " will be above upper border.");
            }
        }
        
        // go
        GameObject tunnelFloor = Spawn4FacePolyAtBottomBorderFromTo(tunnelFloorName, upperLeft, upperRight);
        GameObject tunnelCeiling = SpawnTopBorderObstacleParallelToPolyAtBottom(tunnelCeilingName, upperLeft, upperRight, tunnelHeight);
        return new GameObject[] { tunnelFloor, tunnelCeiling };
    }

    // adapter piece to put between tunnel pieces with different tunnel heights
    public GameObject[] SpawnTunnelHeightAdapterDefinedByBottomFromTo(Vector2 oldFloorEndPoint, float oldTunnelHeight, Vector2 nextFloorStartPoint, float newTunnelHeight)
    {
        Vector2 oldCeilingEndPoint = new Vector2(oldFloorEndPoint.x, (oldFloorEndPoint.y + oldTunnelHeight));
        Vector2 newCeilingStartPoint = new Vector2(nextFloorStartPoint.x, (nextFloorStartPoint.y + newTunnelHeight));

        GameObject tunnelFloor = Spawn4FacePolyAtBottomBorderFromTo("TunnelFloor", oldFloorEndPoint, nextFloorStartPoint);        
        GameObject tunnelCeiling = Spawn4FacePolyAtTopBorderFromTo("TunnelCeiling", oldCeilingEndPoint, newCeilingStartPoint);

        return new GameObject[] { tunnelFloor, tunnelCeiling };
    }

    // spawn a tunnel segment consisting of 3 parts: horizontal start piece of given length, diagonal part, horizontal end piece of given length.
    // if the start and end height is different, the middle part will adapt between them (i.e., the horizontal end segment already has the new height)
    // you can set the length of the horizontal start and/or end segments to 0 if you do not want them
    public GameObject[] SpawnComplexTunnelPart(Vector2 oldFloorEndPointBottom, float oldTunnelHeight, float lengthOfHorizontalStartSegment, Vector2 nextFloorStartPointBottom, float newTunnelHeight, float lengthOfHorizontalEndSegment)
    {
        List<GameObject> parts = new List<GameObject>();

        Vector2 horizontalStartSegmentEndPointBottom = new Vector2(oldFloorEndPointBottom.x + lengthOfHorizontalStartSegment, oldFloorEndPointBottom.y);
        Vector2 horizontalStartSegmentEndPointTop = new Vector2(horizontalStartSegmentEndPointBottom.x, horizontalStartSegmentEndPointBottom.y + oldTunnelHeight);
        // spawn horizontal start segment
        if (lengthOfHorizontalStartSegment >= 0.01F)
        {
            GameObject[] floorAndCeiling = SpawnTunnelSegmentDefinedByBottom(oldFloorEndPointBottom, horizontalStartSegmentEndPointBottom, oldTunnelHeight);
            foreach(GameObject part in floorAndCeiling)
            {
                parts.Add(part);
            }
        }

        // spawn potentially diagonal piece which may also need to adjust the height (i.e., floor and ceiling may NOT be parallel)
        Vector2 horizontalEndSegmentStartPointBottom = new Vector2(nextFloorStartPointBottom.x - lengthOfHorizontalEndSegment, nextFloorStartPointBottom.y);
        Vector2 horizontalEndSegmentStartPointTop = new Vector2(horizontalEndSegmentStartPointBottom.x, horizontalEndSegmentStartPointBottom.y + newTunnelHeight);
        Vector2 nextFloorStartPointTop = new Vector2(nextFloorStartPointBottom.x, nextFloorStartPointBottom.y + newTunnelHeight);

        Vector2 diagonalSegmentStartPointBottom = horizontalStartSegmentEndPointBottom;
        Vector2 diagonalSegmentEndPointBottom = horizontalEndSegmentStartPointBottom;
        GameObject diagonalFloor = Spawn4FacePolyAtBottomBorderFromTo("TunnelFloor", diagonalSegmentStartPointBottom, diagonalSegmentEndPointBottom);
        parts.Add(diagonalFloor);

        Vector2 diagonalSegmentStartPointTop = horizontalStartSegmentEndPointTop;
        Vector2 diagonalSegmentEndPointTop = horizontalEndSegmentStartPointTop;
        GameObject diagonalCeiling = Spawn4FacePolyAtTopBorderFromTo("TunnelCeiling", diagonalSegmentStartPointTop, diagonalSegmentEndPointTop);
        parts.Add(diagonalCeiling);

        // spawn horizontal end segment with new height
        if (lengthOfHorizontalEndSegment >= 0.01F)
        {
            GameObject[] floorAndCeiling = SpawnTunnelSegmentDefinedByBottom(horizontalEndSegmentStartPointBottom, nextFloorStartPointBottom, newTunnelHeight);
            foreach (GameObject part in floorAndCeiling)
            {
                parts.Add(part);
            }
        }

        return parts.ToArray();
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

        

        Vector3 currentPosition = go.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + difference, currentPosition.z);
        Debug.Log("AlignGameObjectLowerBorderToYPosition: Moving GameObject " + go.name + " from position " + currentPosition.ToString() + " to " + targetPosition.ToString() + ". Move distance is " + difference.ToString("n3") + ", initial y min pos was " + currentYMin.ToString("n3") + ", y max was " + currentYMax.ToString("n3") +", target y pos is " + targetYPos.ToString("n3") + ".");

        go.transform.position = targetPosition;

        float afterYMin = go.GetComponent<Renderer>().bounds.min.y;
        float afterYMax = go.GetComponent<Renderer>().bounds.max.y;

        Debug.Log("AlignGameObjectLowerBorderToYPosition: After move. New y min pos is " + afterYMin.ToString("n3") + ", y max is " + afterYMax.ToString("n3") + ".");
    }

    private Vector2[] GetBottomPolyVerticesFromTo(Vector2 upperLeft, Vector2 upperRight, string tag = "")
    {
        if(upperLeft.x >= upperRight.x)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The left vertex is NOT left of the right one.");
        }
        if (upperLeft.y < this.lowerBoarderYPos || upperRight.y < this.lowerBoarderYPos)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The two points must not be below the lower border.");
        }
        if (upperLeft.y == this.lowerBoarderYPos && upperRight.y == this.lowerBoarderYPos)
        {
            Debug.Log("GetBottomPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The two points must not be both directly on the lower border.");
        }
        return new Vector2[] { new Vector2(upperLeft.x, this.lowerBoarderYPos), upperLeft, upperRight, new Vector2(upperRight.x, this.lowerBoarderYPos) };
    }

    public GameObject Spawn4FacePolyAtTopBorderFromTo(string name, Vector2 lowerLeft, Vector2 lowerRight, DeleteVertexToMakeTriangleInsteadOf4FacePoly deleteVertexOption = DeleteVertexToMakeTriangleInsteadOf4FacePoly.KeepAll4Vertices)
    {
        Vector2[] vertices2D = GetTopPolyVerticesFromTo(lowerLeft, lowerRight, name);
        vertices2D = filterVerticesUsingDeleteOption(vertices2D, deleteVertexOption);
        return SpawnObstaclePolygon(name, vertices2D);
    }

    private Vector2[] GetTopPolyVerticesFromTo(Vector2 lowerLeft, Vector2 lowerRight, string tag = "")
    {
        if (lowerLeft.x >= lowerRight.x)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The left vertex is NOT left of the right one.");
        }
        if (lowerLeft.y > this.upperBoarderYPos || lowerRight.y > this.upperBoarderYPos)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The two points must not be above the upper border.");
        }
        if (lowerLeft.y == this.upperBoarderYPos && lowerRight.y == this.upperBoarderYPos)
        {
            Debug.Log("GetTopPolyVerticesFromTo: ERROR: Polygon " + tag + " invalid. The two points must not be both directly on the upper border.");
        }
        return new Vector2[] { new Vector2(lowerLeft.x, this.upperBoarderYPos), lowerLeft, lowerRight, new Vector2(lowerRight.x, this.upperBoarderYPos) };
    }

    public GameObject SpawnTunnelStartRampBottom(float startPosX, Vector2 endPoint)
    {
        if(endPoint.y < this.lowerBoarderYPos)
        {
            Debug.Log("SpawnTunnelStartRampBottom: end point must not be below lower boarder.");
        }
        return Spawn4FacePolyAtBottomBorderFromTo("StartRampBottom", new Vector2(startPosX, lowerBoarderYPos), endPoint, DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex1ToCreateStartRampTriangle);
    }

    public GameObject SpawnTunnelStartRampTop(float startPosX, Vector2 endPoint)
    {
        if (endPoint.y > this.upperBoarderYPos)
        {
            Debug.Log("SpawnTunnelStartRampTop: end point must not be above upper boarder.");
        }
        return Spawn4FacePolyAtTopBorderFromTo("StartRampTop", new Vector2(startPosX, upperBoarderYPos), endPoint, DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex1ToCreateStartRampTriangle);
    }

    public GameObject[] SpawnBothTunnelStartRampsForTunnelDefinedByBottom(float bothRampsStartPosX, Vector2 nextTunnelStartPointBottom, float tunnelHeight)
    {
        if(bothRampsStartPosX >= nextTunnelStartPointBottom.x)
        {
            Debug.Log("SpawnBothTunnelStartRampsForTunnelDefinedByBottom: Ramp end point must not be left of start point.");
        }
        Vector2 nextTunnelStartPointTop = new Vector2(nextTunnelStartPointBottom.x, nextTunnelStartPointBottom.y + tunnelHeight);
        GameObject startRampTop = SpawnTunnelStartRampTop(bothRampsStartPosX, nextTunnelStartPointTop);

        GameObject startRampBottom = SpawnTunnelStartRampBottom(bothRampsStartPosX, nextTunnelStartPointBottom);
        return new GameObject[] { startRampBottom, startRampTop };
    }

    public GameObject SpawnTunnelEndRampBottom(Vector2 startPoint, float endPosX)
    {
        if(startPoint.x >= endPosX)
        {
            Debug.Log("SpawnTunnelEndRampBottom: Start point of end ramp x must not be right of end point.");
        }
        if(startPoint.y < this.lowerBoarderYPos)
        {
            Debug.Log("SpawnTunnelEndRampBottom: Start point of end ramp y must not be below lower border.");
        }
        return Spawn4FacePolyAtBottomBorderFromTo("EndRampBottom", startPoint, new Vector2(endPosX, this.lowerBoarderYPos), DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex3ToCreateEndRampTriangle);
    }

    public GameObject SpawnTunnelEndRampTop(Vector2 startPoint, float endPosX)
    {
        if (startPoint.x >= endPosX)
        {
            Debug.Log("SpawnTunnelEndRampTop: Start point of end ramp x must not be right of end point.");
        }
        if (startPoint.y > this.upperBoarderYPos)
        {
            Debug.Log("SpawnTunnelEndRampTop: Start point of end ramp y must not be above upper border.");
        }
        return Spawn4FacePolyAtTopBorderFromTo("EndRampTop", startPoint, new Vector2(endPosX, this.upperBoarderYPos), DeleteVertexToMakeTriangleInsteadOf4FacePoly.DeleteIndex3ToCreateEndRampTriangle);
    }

    public GameObject[] SpawnBothTunnelEndRampsForTunnelDefinedByBottom(Vector2 lastTunnelEndPointBottom, float bothRampsEndPosX, float tunnelHeight)
    {
        if(lastTunnelEndPointBottom.x >= bothRampsEndPosX)
        {
            Debug.Log("SpawnBothTunnelEndRampsForTunnelDefinedByBottom: Ramp start point must not be left of end point.");
        }
        Vector2 lastTunnelEndPointTop = new Vector2(lastTunnelEndPointBottom.x, lastTunnelEndPointBottom.y + tunnelHeight);
        GameObject endRampTop = SpawnTunnelEndRampTop(lastTunnelEndPointTop, bothRampsEndPosX);

        GameObject endRampBottom = SpawnTunnelEndRampBottom(lastTunnelEndPointBottom, bothRampsEndPosX);
        return new GameObject[] { endRampBottom, endRampTop };
    }
}
