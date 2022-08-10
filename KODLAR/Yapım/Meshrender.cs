using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshrender : MonoBehaviour
{
    private void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triagles = new int[6];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 100);
        vertices[2] = new Vector3(100, 100);
        vertices[3] = new Vector3(100, 0);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triagles[0] = 0;
        triagles[1] = 1;
        triagles[2] = 2;

        triagles[3] = 0;
        triagles[4] = 2;
        triagles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triagles;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
