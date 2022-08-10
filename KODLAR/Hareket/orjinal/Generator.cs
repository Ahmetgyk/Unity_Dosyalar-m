using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Generator : MonoBehaviour
{
    public Noise.NormalizeMode normalizeMode;

    public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int editorPreviewLOD;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMap;

    Queue<MapThreadInfo<DataMap>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<DataMap>>();
    Queue<MapThreadInfo<DataMesh>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<DataMesh>>();

    void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }

    public void DrawMapInEditor()
    {
        DataMap mapData = GenerateMapData(Vector2.zero);

        Display display = FindObjectOfType<Display>();

        display.DrawMesh(MeshG.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorPreviewLOD), TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
    }

    public void RequestMapData(Vector2 centre, Action<DataMap> callback)
    {
        ThreadStart threadStart = delegate {
            MapDataThread(centre, callback);
        };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 centre, Action<DataMap> callback)
    {
        DataMap mapData = GenerateMapData(centre);
        lock (mapDataThreadInfoQueue)
        {
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<DataMap>(callback, mapData));
        }
    }

    public void RequestMeshData(DataMap mapData, int lod, Action<DataMesh> callback)
    {
        ThreadStart threadStart = delegate {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }

    void MeshDataThread(DataMap mapData, int lod, Action<DataMesh> callback)
    {
        DataMesh meshData = MeshG.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod);
        lock (meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<DataMesh>(callback, meshData));
        }
    }

    void Update()
    {
        if (mapDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<DataMap> threadInfo = mapDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if (meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<DataMesh> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    DataMap GenerateMapData(Vector2 centre)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, centre + offset, normalizeMode);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight >= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        return new DataMap(noiseMap, colourMap);
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }

        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }

    }

}

[System.Serializable]
public struct Terrain
{
    public string name;
    public float height;
    public Color colour;
}

public struct DataMap
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public DataMap(float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}

