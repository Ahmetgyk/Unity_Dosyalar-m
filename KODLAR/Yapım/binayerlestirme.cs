using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class binayerlestirme : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wall;
    public GameObject player;

    private bool playerspawn = false;

    private void Start()
    {
        Generatellevel();
    }
    void Generatellevel()
    {
        for(int x = 0; x <= width; x += 2)
        {
            for(int y =0; y <= height; y += 2)
            {
                if (Random.value > 0.7f)
                {
                    Vector3 pos = new Vector3(x - width / 2f, Random.Range(1f, 5f), y - height / 2f);
                    Instantiate(wall, pos, Quaternion.identity, transform);
                }
                else if (!playerspawn)
                {
                    Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                    Instantiate(wall, pos, Quaternion.identity, transform);
                    playerspawn = true;
                }
            }
        }
    }
}
