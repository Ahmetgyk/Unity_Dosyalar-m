using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bölgeler : MonoBehaviour
{
    public int width = 60;
    public int height = 60;
    private float deger;
    public int a = 3;
    private int O;

    public float H;
    public int tane;

    public Transform parent;
    public Transform binaparent;
    public Transform duvarparent;

    public GameObject Bina;
    public GameObject player;
    public GameObject Duv;
    public GameObject Sur;

    List<GameObject> surobj = new List<GameObject>();
    List<Transform> duvarobj = new List<Transform>();

    private bool playerspawn = false;

    private void Start()
    {
        parent = transform;
        binaparent = new GameObject().transform;
        duvarparent = new GameObject().transform;
        binaparent.name = "Binalar";
        duvarparent.name = "Duvarlar";
        binaparent.transform.parent = parent;
        duvarparent.transform.parent = parent;

        // Generatellevel();
        Altıgen();
        Duvar();
    }
    void Generatellevel()
    {
        for(int x = 0; x <= width; x += 2)
        {
            for(int y =0; y <= height; y += 2)
            {
                O = Random.Range(0, 5);
                float degerx;
                float degery;
                degerx = (x <= width / 2) ? width / 2f - x : x - width / 2;
                degery = (y <= height / 2) ? height / 2f - y : y - height / 2;
                deger = (degerx + degery) * 0.05f;

                if (Random.value > deger)
                {
                    Vector3 pos = new Vector3(x - width / 2f, 1, y - height / 2f);
                    Instantiate(Bina, pos, Quaternion.Euler(0 ,90*a, 0), transform);
                }
                else if (!playerspawn)
                {
                    Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                    Instantiate(Bina, pos, Quaternion.identity, transform);
                    playerspawn = true;
                }
            }
        }
    }
    void Altıgen()
    {
        int x = -6 * (a / 2);
        int y = -5 * (a / 2);
        int Ay;
        int By;

        int boşluk = a - 1;
        int bina = a;
        for(Ay = 0; Ay < 2 * a; Ay += 2)
        {
            for(int b = 0; b < boşluk; b++)
            {
                x += 2;
            }
            for(int Ax = 0; Ax < bina; Ax++)
            {
                x += 2;

                O = Random.Range(0, 5);
                Vector3 pos = new Vector3(x, 1f, y + Ay);
                float deger = Vector3.Distance(transform.position, pos) * 0.05f;

                if (Random.value > deger)
                {
                    GameObject binaGo = Instantiate(Bina, pos, Quaternion.Euler(0, 90 * O, 0), transform);
                    binaGo.transform.parent = binaparent;
                }

            }
            boşluk--;
            bina += 2;
            x = -6 * (a / 2);
        }
        y += Ay;
        for (By = 0; By < 2 * (a - 2); By += 2)
        {
            for(int Bx = 0; Bx < (3 * a)  - 2; Bx++)
            {
                x += 2;

                O = Random.Range(0, 5);
                Vector3 pos = new Vector3(x, 1f, y + By);
                float deger = Vector3.Distance(transform.position, pos) * 0.035f;

                if (Random.value > deger)
                {
                    GameObject binaGo = Instantiate(Bina, pos, Quaternion.Euler(0, 90 * O, 0), transform);
                    binaGo.transform.parent = binaparent;
                }
            }
            x = -6 * (a / 2);
        }
        y += By;
        boşluk = 0;
        bina = (3 * a) - 2;
        for (int Cy = 0; Cy < 2 * a; Cy += 2)
        {
            for (int b = 0; b < boşluk; b++)
            {
                x += 2;
            }
            for (int Cx = 0; Cx < bina; Cx++)
            {
                x += 2;

                O = Random.Range(0, 5);
                Vector3 pos = new Vector3(x, 1f, y + Cy);
                float deger = Vector3.Distance(transform.position, pos) * 0.05f;

                if (Random.value > deger)
                {
                    GameObject binaGo = Instantiate(Bina, pos, Quaternion.Euler(0, 90 * O, 0), transform);
                    binaGo.transform.parent = binaparent;
                }
            }
            boşluk++;
            bina -= 2;
            x = -6 * (a / 2);
        }
    }
    void Duvar()
    {
        H = 0;
        tane = Random.Range(5, 9);

        for (int Rot = 0; Rot <= tane; Rot++)
        {
            H += 360 / tane;

            transform.localRotation = Quaternion.Euler(0, H, 0);

            Vector3 pos = new Vector3(transform.position.x + a * 3.5f, transform.position.y + 1, transform.position.z);
            GameObject duvar = Instantiate(Duv, pos, Quaternion.identity, transform);
            GameObject sur = Instantiate(Sur, pos, Quaternion.identity, transform);
            duvar.transform.parent = duvarparent;
            sur.transform.parent = duvarparent;
            duvarobj.Add(duvar.transform);
            surobj.Add(sur);
        }
        for(int s = 0; s < tane; s++)
        {
            surobj[s].GetComponent<Duvar>().Sur1 = duvarobj[0 + s];
            surobj[s].GetComponent<Duvar>().Sur2 = duvarobj[1 + s];
        }
        H = 0;
    }
}
