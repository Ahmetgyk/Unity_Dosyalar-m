using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{

    public List<GameObject> array = new List<GameObject>();
    public int howMany;
    private int howManyTrees;

    public List<float> X = new List<float>();
    public List<float> Y = new List<float>();

    public int x = 0;
    private int y = 0;

    public GameObject[] houses;
    public GameObject[] trees;

    private Town town;

    // Start is called before the first frame update
    void Start()
    {
        town = GameObject.Find("Road").GetComponent<Town>();
    }

    public void Add(GameObject x)
    {
        array.Add(x);
    }

    public void AddX(float x)
    {
        X.Add(x);
    }

    public void AddY(float x)
    {
        Y.Add(x);
    }

    // Update is called once per frame
    void Update()
    {
        if (array.Count > howMany || x == 1)
        {

            if (y == 0)
            {
                y = 1;
                Houses();

            }

        }
    }

    void Houses()
    {
        for (int i = 0; i < array.Count; i++)
        {
            Instantiate(houses[Random.Range(0, houses.Length)], array[i].GetComponent<Town>().positions[0].transform.position, Quaternion.identity);
            Instantiate(houses[Random.Range(0, houses.Length)], array[i].GetComponent<Town>().positions[1].transform.position, Quaternion.identity);
            Instantiate(houses[Random.Range(0, houses.Length)], array[i].GetComponent<Town>().positions[2].transform.position, Quaternion.identity);
            Instantiate(houses[Random.Range(0, houses.Length)], array[i].GetComponent<Town>().positions[3].transform.position, Quaternion.identity);
        }

        howManyTrees = array.Count * 3;

        X.Sort();
        Y.Sort();

        for (int i = 0; i < howManyTrees; i++)
        {
            Instantiate(trees[Random.Range(0, trees.Length)], new Vector2(Random.Range(X[0] - 20f, X[X.Count - 1] + 20f), 
            Random.Range(Y[0] - 20f, Y[Y.Count - 1] + 20f)), Quaternion.identity);
        }
    }
}
