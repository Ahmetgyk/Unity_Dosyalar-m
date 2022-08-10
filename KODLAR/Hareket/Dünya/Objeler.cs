using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeler : MonoBehaviour
{
    public GameObject parent;
    public bool Generate = false;
    private bool bit;
    Vector3 vec;
    int z_deger;

    public void kordinat(Terrain[] terrain, float[,] noiseMap, int uzunluk, float mesafe)
    {
        if (Generate)
        {
            parent = new GameObject().gameObject;
            parent.name = "levelparent";

            Vector3 vertices;

            for (int y = 0; y < uzunluk; y++)
            {
                for (int x = 0; x < uzunluk; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    bit = true;
                    for (int i = terrain.Length - 1; i >= 0; i--)
                    {
                        if (currentHeight >= terrain[i].Height)
                        {                           
                            vertices = new Vector3(mesafe * x + .5f, 0, mesafe * y + .5f);
                            terrain[i].Vectors.Add(vertices);
                            bit = false;
                        }
                        if (!bit)
                        {
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < terrain.Length; i++)
            {
                for(int j = 0; j < terrain[i].Vectors.Count; j++)
                {
                    GameObject tileobject;
                    tileobject = Instantiate(terrain[i].Object, terrain[i].Vectors[j], transform.rotation);
                    tileobject.GetComponent<MeshRenderer>().sharedMaterial.color = terrain[i].Color;
                    tileobject.transform.parent = parent.transform;
                    z_deger = (int)terrain[i].Vectors[j].z;

                    for (float a = 0; a < tileobject.transform.localScale.x; a++)
                    {
                        Vector3 rvec = new Vector3(tileobject.transform.position.x - (tileobject.transform.localScale.x / 2f) + a + 0.5f, 0.5f, z_deger + 0.5f);
                        terrain[i].Vectors.Remove(rvec);
                    }
                }
            }
        }
    }
}
