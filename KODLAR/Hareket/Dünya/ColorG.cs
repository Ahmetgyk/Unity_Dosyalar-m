using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorG
{
    public static float[,] GenerateColour(int mapWidht, int mapHeight, float scale, int octave, float peristance, float lacunarity, int seed, Vector2 offset, GameObject meshO, Terrain[] terrain)
    {
        float[,] noiseMap = new float[mapWidht, mapHeight];
        float[,] map = new float[mapWidht, mapHeight];

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
        float halfWidht = mapWidht / 2f;
        float halfHeight = mapHeight / 2f;
        float a = 3;
        float b = 2.2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidht; x++)
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

                    float X = x / (float)mapWidht * 2 - 1;
                    float Y = y / (float)mapHeight * 2 - 1;

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
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidht; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);

                noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - map[x, y]);
            }
        }

        Color[] colourMap = new Color[mapWidht * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidht; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight >= terrain[i].Height)
                    {
                        colourMap[y * mapWidht + x] = terrain[i].Color;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        Texture2D texture = new Texture2D(mapWidht, mapHeight);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        meshO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
        texture.Apply();
        return noiseMap;
    }
}
