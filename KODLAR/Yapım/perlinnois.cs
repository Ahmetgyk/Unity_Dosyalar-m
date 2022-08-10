using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinnois : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float offsetx = 100f;
    public float offsety = 100f;

    private void Start()
    {
        offsetx = Random.Range(0f, 999f);
        offsety = Random.Range(0f, 999f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            offsetx = Random.Range(0f, 999f);
            offsety = Random.Range(0f, 999f);
        }
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = Generatertexture();
    }
    Texture2D Generatertexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Color color = Calcutecolor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
    Color Calcutecolor(int x, int y)
    {
        float xcoord = (float)x / width * scale + offsetx;
        float ycoord = (float)y / height * scale + offsety;

        float sample = Mathf.PerlinNoise(xcoord, ycoord);
        return new Color(sample, sample, sample);
    }
}
