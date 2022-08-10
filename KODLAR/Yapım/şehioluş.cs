using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class şehioluş : MonoBehaviour
{
    public Transform parent;

    public GameObject[] tiles;
    public GameObject wall;
    public GameObject player;
    public GameObject enemy;

    public List<Vector3> createdtiles;

    public int enemyamount = 10;
    public int tileamount;
    public int tilesize;
    public float waittime;
    public float chanceright;
    public float chanceup;
    public float changedown;

    public float miny = 999999;
    public float maxy = 0;
    public float minx = 999999;
    public float maxx = 0;
    public float xammount;
    public float yammount;
    public float extrawallx;
    public float extrawally;

    private void Start()
    {
        parent = new GameObject().transform;
        parent.name = "levelparent";
        StartCoroutine(Generatelevel());
    }
    IEnumerator Generatelevel()
    {
        for (int i = 0; i < tileamount; i++)
        {
            int dir = Random.Range(0, 5);
            int tile = Random.Range(0, tiles.Length);
            Createtile(tile);
            Movegen(dir);
            yield return new WaitForSeconds(waittime);

            if (i == tileamount - 1)
            {
                Finis();
            }
        }
        yield return 0;
    }
    void Callmovegen(float rangir)
    {
        if (rangir < chanceup)
        {
            Movegen(0);
        }
        else if (rangir < chanceright)
        {
            Movegen(1);
        }
        else if (rangir < changedown)
        {
            Movegen(2);
        }
        else
        {
            Movegen(3);
        }
    }
    void Movegen(int dir)
    {
        switch (dir)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + tilesize);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + tilesize, transform.position.y, 0);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z - tilesize);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - tilesize, transform.position.y, 0);
                break;

        }
    }
    void Createtile(int tileındex)
    {
        if (!createdtiles.Contains(transform.position))
        {
            GameObject tileobject;
            tileobject = Instantiate(tiles[tileındex], transform.position, transform.rotation) as GameObject;
            createdtiles.Add(tileobject.transform.position);
            tileobject.transform.parent = parent;
        }
        else
        {
            tileamount++;
        }
    }
    void Finis()
    {
        Createwallvalues();
        createwall();
        spawnobject();
    }
    void spawnobject()
    {
        Instantiate(player, createdtiles[Random.Range(0, createdtiles.Count)], Quaternion.identity);
        for (int i = 0; i < enemyamount; i++)
        {
            Instantiate(enemy, createdtiles[Random.Range(0, createdtiles.Count)], Quaternion.identity);
        }
    }
    void Createwallvalues()
    {
        for (int i = 0; i < createdtiles.Count; i++)
        {
            if (createdtiles[i].y < miny)
            {
                miny = createdtiles[i].z;
            }
            if (createdtiles[i].y > maxy)
            {
                maxy = createdtiles[i].z;
            }
            if (createdtiles[i].y < minx)
            {
                minx = createdtiles[i].x;
            }
            if (createdtiles[i].y > maxx)
            {
                maxx = createdtiles[i].x;
            }
            xammount = ((maxx - minx) / tilesize) + extrawallx;
            yammount = ((maxy - miny) / tilesize) + extrawally;
        }
    }
    void createwall()
    {
        for (int x = 0; x < xammount; x++)
        {
            for (int y = 0; y < yammount; y++)
            {
                if (!createdtiles.Contains(new Vector3((minx - (extrawallx * tilesize) / 2) + (x * tilesize), (miny - (extrawally * tilesize) / 2) + (y * tilesize))))
                {
                    GameObject wallobj = (GameObject)Instantiate(wall, new Vector3((minx - (extrawallx * tilesize) / 2) + (x * tilesize), 0, (miny - (extrawally * tilesize) / 2) + (y * tilesize)), transform.rotation);
                    wallobj.transform.parent = parent;
                }
            }
        }
    }
}
