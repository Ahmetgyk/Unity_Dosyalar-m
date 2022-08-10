using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MeshGenerator 
{
    public static Datamesh MeshGen( int uzunluk, float mesafe, float heigh, GameObject mesh, AnimationCurve anim, float[,] noiseMap)
    {
        Datamesh data = new Datamesh(uzunluk);

        int vert = 0;
        int tris = 0;
        for (int i = 0, y = 0; y <= uzunluk; y++)
        {
            for (int x = 0; x <= uzunluk; x++)
            { 
                data.vertices[i] = new Vector3(mesafe * x, 0, mesafe * y);

                i++;
            }
        }
        for (int i = 0, y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                data.vertices[i].y = anim.Evaluate(noiseMap[x, y]) * heigh;

                i++;
            }
        }
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                data.triangles[tris + 0] = vert + 0;
                data.triangles[tris + 1] = vert + uzunluk + 1;
                data.triangles[tris + 2] = vert + 1;

                data.triangles[tris + 3] = vert + 1;
                data.triangles[tris + 4] = vert + uzunluk + 1;
                data.triangles[tris + 5] = vert + uzunluk + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        for (int i = 0, y = 0; y <= uzunluk; y++)
        {
            for (int x = 0; x <= uzunluk; x++)
            {
                data.uvs[i] = new Vector2((float)x / uzunluk, (float)y / uzunluk);

                i++;
            }
        }

        data.mesh.vertices = data.vertices;
        data.mesh.uv = data.uvs;
        data.mesh.triangles = data.triangles;
        data.mesh.RecalculateNormals();
        mesh.GetComponent<MeshFilter>().mesh = data.mesh;
        return data;
    }   
}
public class Datamesh 
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangles;
    public Mesh mesh = new Mesh();

    public Datamesh(int uzunluk)
    {
        vertices = new Vector3[(uzunluk + 1) * (uzunluk + 1)];
        triangles = new int[uzunluk * uzunluk * 6];
        uvs = new Vector2[(uzunluk + 1) * (uzunluk + 1)];
    }

}

