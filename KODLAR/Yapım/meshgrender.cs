using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class meshgrender : MonoBehaviour
{
    Mesh mesh;
    private MeshCollider meshcollider;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    float minterrainheight;
    float maxterrainheight;

    public Gradient gradient;

    public int XSize = 20;
    public int ZSize = 20;

    public float amp = 10f;
    public float freq = 10f;
    public int seed = 0;

    private Vector3 myPos;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        myPos = this.transform.position;

        CreateShape();
        UpdateMesh();
    }
    private void Start()
    {
        meshcollider = gameObject.AddComponent<MeshCollider>();
    }
    void CreateShape()
    {
        vertices = new Vector3[(XSize + 1) * (ZSize + 1)];

        for(int i = 0, z = 0; z <= ZSize; z++)
        {
            for(int x = 0; x <= XSize; x++)
            {
                float y = 0;

                y = Mathf.PerlinNoise((seed + myPos.x + x) / (freq * 4f), (myPos.z + z) / (freq * 4f)) * amp * 4f;

                y += Mathf.PerlinNoise((seed + myPos.x + x) / freq, (myPos.z + z) / freq) * amp;

                y += Mathf.PerlinNoise((seed + myPos.x + x) / freq, (myPos.z + z) / (freq * 0.5f)) * (amp * 0.5f);
                vertices[i] = new Vector3(x, y, z);

                if (y > maxterrainheight)
                {
                    maxterrainheight = y;
                }
                if (y < minterrainheight)
                {
                    minterrainheight = y;
                }
                i++;
            }
        }

        triangles = new int[XSize * ZSize * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < ZSize; z++)
        {
            for (int x = 0; x < XSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + XSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + XSize + 1;
                triangles[tris + 5] = vert + XSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        colors = new Color[vertices.Length];

        for(int i = 0, z = 0; z <= ZSize; z++)
        {
            for(int x = 0; x <= XSize; x++)
            {
                float height = Mathf.InverseLerp(minterrainheight, maxterrainheight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals(); 
    }

}
