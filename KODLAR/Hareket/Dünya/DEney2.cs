using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEney2 : MonoBehaviour
{
    public terrain[] terrain;

    public int octave, seed, uzunluk, mesafe;
    public float scale,  peristance,  lacunarity;
    public Vector2 offset;
    public float[] deger;

    public AnimationCurve anim;
    public float heigh;
    public GameObject deniz;

    float[,] noiseMap;
    float[,] map;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    private void Start()
    {
        Col();
        Mes();
        deniz.transform.localScale = new Vector3(uzunluk * mesafe / 10, 0.1f, uzunluk * mesafe / 10);
        deniz.transform.position = new Vector3(uzunluk * mesafe / 2, 1.1f, uzunluk * mesafe / 2);
    }
    void Col()
    {
        deger = new float[uzunluk * uzunluk];
        noiseMap = new float[uzunluk, uzunluk];
        map = new float[uzunluk, uzunluk];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octave];
        for (int i = 0; i < octave; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        float halfWidht = uzunluk / 2f;
        float halfHeight = uzunluk / 2f;
        float a = 3;
        float b = 2.2f;

        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octave; i++)
                {
                    float sampleX = (x - halfWidht) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= peristance;
                    frequency *= lacunarity;

                    float X = x / (float)uzunluk * 2 - 1;
                    float Y = y / (float)uzunluk * 2 - 1;

                    float value = Mathf.Max(Mathf.Abs(X), Mathf.Abs(Y));
                    map[x, y] = Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);

                noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - map[x, y]);
            }
        }
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                deger[y * uzunluk + x] = noiseMap[x, y];
            }
        }

        Color[] colourMap = new Color[uzunluk * uzunluk];
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight >= terrain[i].Height)
                    {
                        colourMap[y * uzunluk + x] = terrain[i].Color;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        Texture2D texture = new Texture2D(uzunluk, uzunluk);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
        texture.Apply();
    }
    void Mes()
    {
        vertices = new Vector3[(uzunluk + 1) * (uzunluk + 1)];
        triangles = new int[uzunluk * uzunluk * 6];
        uvs = new Vector2[(uzunluk + 1) * (uzunluk + 1)];

        Datamesh data = new Datamesh(uzunluk);

        int vert = 0;
        int tris = 0;
        for (int i = 0, y = 0; y <= uzunluk; y++)
        {
            for (int x = 0; x <= uzunluk; x++)
            {
                if (y < uzunluk && x < uzunluk)
                {
                    data.vertices[i] = new Vector3(mesafe * x, anim.Evaluate(deger[y * uzunluk + x]) * heigh, mesafe * y);
                }
                else
                {
                    data.vertices[i] = new Vector3(mesafe * x, 0, mesafe * y);
                }
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
        GetComponent<MeshFilter>().mesh = data.mesh;
        GetComponent<MeshCollider>().sharedMesh = data.mesh;
    }
    void Mes1()
    {
        vertices = new Vector3[(uzunluk + 1) * (uzunluk + 1)];
        triangles = new int[uzunluk * uzunluk * 6];
        uvs = new Vector2[(uzunluk + 1) * (uzunluk + 1)];

        Datamesh data = new Datamesh(uzunluk);

        int vert = 0;
        int tris = 0;
        for (int i = 0, y = 0; y <= uzunluk - 30; y++)
        {
            for (int x = 0; x <= uzunluk - 30; x++)
            {
                data.vertices[i] = new Vector3(mesafe * x, 0, mesafe * y);
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
        GetComponent<MeshFilter>().mesh = data.mesh;
    }
}
[System.Serializable]
public struct terrain
{
    public string Name;
    public float Height;
    public Color Color;
}

