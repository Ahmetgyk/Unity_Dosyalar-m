using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binalar : MonoBehaviour
{
    public GameObject binaPr;
    public GameObject bölge;

    public Transform parent;

    public int a;
    public float dist;

    List<GameObject> bina = new List<GameObject>();

    private void Start()
    {
        bölge = GameObject.FindGameObjectWithTag("bölge");
        parent = bölge.transform;
        dist = 2 * bölge.GetComponent<bölgeler>().a - Vector3.Distance(transform.position, bölge.gameObject.transform.position);
        dist =  Mathf.Round(dist);
        int deger = (int)dist;
        a = Random.Range(deger , 5);
        Generatellevel();
    }
    void Generatellevel()
    {
        for (int x = 0; x <= 1; x += 1)
        {
            for (int y = 0; y <= 1; y += 1)
            {
                if (bina.Count < a) 
                {
                    dist = Vector3.Distance(transform.position, bölge.gameObject.transform.position);
                    float yükseklik = 2 * bölge.GetComponent<bölgeler>().a - dist;
                    Vector3 pos = new Vector3(transform.position.x - x + 1.5f,0f , transform.position.z - y + 1.5f);
                    GameObject binaGo = Instantiate(binaPr, pos, Quaternion.Euler(0, 90 * Random.Range(0, 3), 0));
                    binaGo.transform.localScale = new Vector3(0.8f, Random.Range(yükseklik / 2, yükseklik) , 0.8f);
                    binaGo.transform.parent = parent;
                    bina.Add(binaPr);        
                }
            }
        }
        foreach(GameObject binaboj in bina)
        {
            float X = transform.position.x;
            string XN = X.ToString();
            float Y = transform.position.x;
            string YN = X.ToString();

            binaboj.GetComponent<binaayarları>().Durum();
            binaboj.GetComponent<binaayarları>().Name = XN + YN;
        }
    }
}
