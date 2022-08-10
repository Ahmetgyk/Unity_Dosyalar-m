using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public GameObject go, go1;
    public Transform parent;
    public List<Vector3> Vector;
    Vector3 vertices;
    public int uzunluk;

    private void Awake()
    {
        Vector.Clear();
    }
    void Start()
    {
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                vertices = new Vector3(x, 0, y + 10);
                Vector.Add(vertices);
            }
        }
        for (int j = 0; j < Vector.Count; j++)
        {
            GameObject tileobject;
            tileobject = Instantiate(go, Vector[j], transform.rotation);
            tileobject.transform.parent = parent.transform;
            for (float a = 0; a < (tileobject.transform.localScale.x * 2) - 1; a++)
            {
                Vector3 rvec = new Vector3(tileobject.transform.position.x - (tileobject.transform.localScale.x - 1) + a, 0, tileobject.transform.position.z);
                Instantiate(go1, rvec, transform.rotation);
                Vector.Remove(rvec);
            }
            for (float a = 0; a < (tileobject.transform.localScale.z * 2) - 1; a++)
            {
                Vector3 rvec = new Vector3(tileobject.transform.position.x, 0, tileobject.transform.position.z - (tileobject.transform.localScale.z - 1) + a);
                Instantiate(go1, rvec, transform.rotation);
                Vector.Remove(rvec);
            }
        }
    }
    void A()
    {
        for (int y = 0; y < uzunluk; y++)
        {
            for (int x = 0; x < uzunluk; x++)
            {
                vertices = new Vector3(x, 0, y + 10);
                Vector.Add(vertices);
            }
        }
        for (int j = 0; j < Vector.Count; j++)
        {
            GameObject tileobject;
            tileobject = Instantiate(go, Vector[j], transform.rotation);
            tileobject.transform.parent = parent.transform;

            for (float a = 0; a < (tileobject.transform.localScale.x * 2) - 1; a++)
            {
                Vector3 rvec = new Vector3(tileobject.transform.position.x - (tileobject.transform.localScale.x - 1) + a, 0, 10 + tileobject.transform.position.x - (tileobject.transform.localScale.x - 1) + a);
                Vector.Remove(rvec);
            }
            j--;
        }
    }
    void B()
    {
        for (int y = 0; y < uzunluk / go.transform.localScale.x; y++)
        {
            for (int x = 0; x < uzunluk / go.transform.localScale.x; x++)
            {
                vertices = new Vector3(go.transform.localScale.x * x, 0, (go.transform.localScale.x * y) + 10);
                Vector.Add(vertices);
                Instantiate(go, vertices, transform.rotation);
            }
        }
    }
}
