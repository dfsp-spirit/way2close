using UnityEngine;
using System.Collections;

public class PolygonSpawner : MonoBehaviour {

    string resultingGameObjectTag;   // e.g., "Obstacle"
    string resultingGameObjectSortingLayerName = null;  // e.g., "Front"
    Material resultingGameObjectMeshMaterial = null;
    Vector3 resultingGameObjectSpawnPosition;
    ColliderType resultingGameObjectColliderType;

    public enum ColliderType { None, Mesh3D, Box2D, Polygon2D };

    public PolygonSpawner()
    {
        this.SpawnPosition = new Vector3(0.0F, 0.0F, 0.0F);
        this.ResultingGameObjectColliderType = ColliderType.Polygon2D;
    }

    

    public string ResultingGameObjectSortingLayerName
    {
        get
        {
            return resultingGameObjectSortingLayerName;
        }

        set
        {
            resultingGameObjectSortingLayerName = value;
        }
    }

    public string ResultingGameObjectTag
    {
        get
        {
            return resultingGameObjectTag;
        }

        set
        {
            resultingGameObjectTag = value;
        }
    }

    public Material Material
    {
        get
        {
            return resultingGameObjectMeshMaterial;
        }

        set
        {
            resultingGameObjectMeshMaterial = value;
        }
    }

    public Vector3 SpawnPosition
    {
        get
        {
            return resultingGameObjectSpawnPosition;
        }

        set
        {
            resultingGameObjectSpawnPosition = value;
        }
    }

    public ColliderType ResultingGameObjectColliderType
    {
        get
        {
            return resultingGameObjectColliderType;
        }

        set
        {
            resultingGameObjectColliderType = value;
        }
    }

    public void SetMaterialByResourceName(string name)
    {
        this.resultingGameObjectMeshMaterial = Resources.Load(name, typeof(Material)) as Material;
    }

    private Mesh GenerateMeshFrom3DVerticesAndIndices(Vector3[] vertices, int[] indices)
    {
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();
        return msh;
    }

    private Vector3[] Generate3DVerticesFrom2DVertices(Vector2[] vertices2D)
    {
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }
        return vertices;
    }

    private void AddTagAndLayer(GameObject objToSpawn)
    {
        if (this.resultingGameObjectTag != null)
        {
            objToSpawn.tag = this.resultingGameObjectTag;
        }

        if (this.resultingGameObjectSortingLayerName != null)
        {
            objToSpawn.GetComponent<MeshRenderer>().sortingLayerName = this.resultingGameObjectSortingLayerName;
        }
    }

    // try something like the following as a vertices2[] as argument: Vector2[] vertices2D = new Vector2[] { new Vector2(0,0), new Vector2(2,2), new Vector2(4,2), new Vector2(6,0) };
    public GameObject SpawnPolygon(string name, Vector2[] vertices2D)
    {
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        Vector3[] vertices3D = Generate3DVerticesFrom2DVertices(vertices2D);

        Mesh msh = GenerateMeshFrom3DVerticesAndIndices(vertices3D, indices);

        GameObject objToSpawn = new GameObject(name);

        objToSpawn.AddComponent(typeof(MeshRenderer));        
        objToSpawn.GetComponent<MeshRenderer>().material = this.resultingGameObjectMeshMaterial;

        MeshFilter filter = objToSpawn.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        AddTagAndLayer(objToSpawn);

        // add collider
        if(this.ResultingGameObjectColliderType != ColliderType.None)
        {
            if (this.ResultingGameObjectColliderType == ColliderType.Mesh3D)
            {
                MeshCollider collider = objToSpawn.AddComponent(typeof(MeshCollider)) as MeshCollider;
                collider.convex = true;
                collider.isTrigger = true;
                collider.sharedMesh = filter.sharedMesh;
            }
            else if(this.ResultingGameObjectColliderType == ColliderType.Box2D)
            {
                BoxCollider2D collider = objToSpawn.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            }
            else if(this.ResultingGameObjectColliderType == ColliderType.Polygon2D)
            {
                PolygonCollider2D collider = objToSpawn.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                collider.pathCount = 1;
                collider.SetPath(0, vertices2D);
            }
        }

        // adjust spawn position
        objToSpawn.transform.Translate(this.resultingGameObjectSpawnPosition);

        return objToSpawn;
    }
}
