using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public float width = 2f;
    public float length = 10f;

    void Start()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-width / 2, 0, 0),
            new Vector3(width / 2, 0, 0),
            new Vector3(-width / 2, 0, length),
            new Vector3(width / 2, 0, length)
        };

        int[] triangles = new int[6]
        {
            0, 2, 1, // First triangle
            2, 3, 1  // Second triangle
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
