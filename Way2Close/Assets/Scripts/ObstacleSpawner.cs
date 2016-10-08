using UnityEngine;
using System.Collections;

public class ObstacleSpawner : PolygonSpawner {

    float obstacleSpeed = 2.0F;

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

    public GameObject SpawnObstaclePolygon(string name, Vector2[] vertices2D)
    {
        GameObject spawnedGameObject = SpawnPolygon(name, vertices2D);
        EnemyMove em = spawnedGameObject.AddComponent(typeof(EnemyMove)) as EnemyMove;
        em.speed = this.ObstacleSpeed;

        return spawnedGameObject;
    }
}
