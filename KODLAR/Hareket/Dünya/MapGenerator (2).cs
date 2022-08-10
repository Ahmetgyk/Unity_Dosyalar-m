using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject meshO;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;
    private float[,] noiseMap;
    public Terrain[] terrain;

    public bool Update;
    public int uzunluk;
    public float mesafe;

    public float scale = 10;
    public int octave = 4;
    public float peristance = 2;
    public float lacunarity = 1;
    public int seed;
    public Vector2 offset;
    public AnimationCurve anim;
    public float heigh;
    int i = 0;

    public void Generate()
    {
        Delet();

        noiseMap = ColorG.GenerateColour(uzunluk, uzunluk, scale, octave, peristance, lacunarity, seed, offset, meshO, terrain);
        MeshGenerator.MeshGen(uzunluk, mesafe, heigh, meshO, anim, noiseMap);
        GetComponent<Objeler>().kordinat(terrain, noiseMap, uzunluk, mesafe);     
    }
    public void Delet()
    {
        GameObject obje = GetComponent<Objeler>().parent;
        DestroyImmediate(obje);

        for (int i = 0; i < terrain.Length; i++)
        {
            terrain[i].Vectors.Clear();
        }
    }
}
[System.Serializable]
public struct Terrain
{
    public string Name;
    public float Height;
    public Color Color;
    public GameObject Object;
    public List<Vector3> Vectors;
}
