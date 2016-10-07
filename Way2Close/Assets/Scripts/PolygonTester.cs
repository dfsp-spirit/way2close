using UnityEngine;

public class PolygonTester : MonoBehaviour
{
    void Start()
    {
        // Create Vector2 vertices
        Vector2[] vertices2D = new Vector2[] {
            new Vector2(0,0),
            new Vector2(2,2),
            new Vector2(4,2),
            new Vector2(6,0)
        };

        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        //for (int i = vertices.Length - 1; i <= 0; i++)
        //{
        //    vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        //}

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        gameObject.AddComponent(typeof(MeshRenderer));
        gameObject.tag = "Obstacle";
        Material obstMatRed = Resources.Load("ObstacleMaterialRed", typeof(Material)) as Material;
        GetComponent<MeshRenderer>().material = obstMatRed;
        GetComponent<MeshRenderer>().sortingLayerName = "Front";
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        // add a mesh collider, will work with 3D objects only
        bool use3DCollider = false;
        if (use3DCollider)
        {
            MeshCollider collider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            collider.convex = true;
            collider.isTrigger = true;
            collider.sharedMesh = filter.sharedMesh;
        }
        else
        {
            BoxCollider2D collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        }

        // move to position
        transform.Translate(new Vector3(-6.0F, 2.0F, 0.0F));

        // add a child gameobject with a 2D collider
    }
}