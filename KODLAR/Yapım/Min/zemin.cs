using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zemin : MonoBehaviour
{
    private GameObject currentBlockType;
    public GameObject[] blockTypes;

    [Tooltip("True for minecraft style voxels")]
    public bool SnapToGrid = true;

    public int boyut;
    public float amp = 10f;
    public float freq = 10f;
    public int seed = 0;

    private Vector3 myPos;

    void Start()
    {
        myPos = this.transform.position;

        generateTerrain();
    }
    void terrainFeatures()
    {
        freq = Mathf.Sin(seed * 12f);
        amp = freq * 100f;
    }
    void generateTerrain()
    {
        int cols = boyut;
        int rows = boyut;

        for (int x = 0; x < cols; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                bool makeTree = false;

                float y = 0;

                y = Mathf.PerlinNoise((seed + myPos.x + x) / (freq * 4f),
                    (myPos.z + z) / (freq * 4f)) * amp * 4f;

                y += Mathf.PerlinNoise((seed + myPos.x + x) / freq,
                    (myPos.z + z) / freq) * amp;

                y += Mathf.PerlinNoise((seed + myPos.x + x) / freq,
                    (myPos.z + z) / (freq * 0.5f)) * (amp * 0.5f);


                if (SnapToGrid)
                {
                    y = Mathf.Floor(y);
                }

                if (y > 24f)
                    currentBlockType =
                        blockTypes[1];
                else
                    currentBlockType =
                        blockTypes[0];

                GameObject newBlock = GameObject.Instantiate(currentBlockType);

                newBlock.transform.SetParent(this.transform);

                float Bfreq = 100f;

                if (myPos.z + z > 70f + Random.Range(-5, 5) && (Mathf.PerlinNoise((seed + myPos.x + x) / Bfreq, (myPos.z + z) / Bfreq) > 0.3f))
                {
                    newBlock.GetComponent<Renderer>().material.color = Color.white;
                }

                Bfreq = 24f;
                if (Mathf.PerlinNoise((seed + myPos.x + x) / Bfreq, (myPos.z + z) / Bfreq) > 0.5f && myPos.z + z < 42f + Random.Range(-5, 5))
                {
                    newBlock.GetComponent<Renderer>().material.color = Color.green;
                    makeTree = true;
                    //						new Color (	0.9f + Random.value/10f, 
                    //									0.9f+ Random.value/10f, 
                    //									1f+ Random.value/10f);
                    //y += 2f;
                }

                newBlock.transform.position =
                    new Vector3(myPos.x + x * currentBlockType.transform.localScale.x,
                        y, myPos.z + z * currentBlockType.transform.localScale.z);

                if (Random.value * 100 < 1 && makeTree == true)
                {
                    float adjust = newBlock.transform.lossyScale.y / 2f;

                    GameObject treeBabe = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    Vector3 tT = treeBabe.transform.localScale;
                    tT.y = Random.value * 24;
                    treeBabe.transform.localScale = tT;

                    adjust += treeBabe.transform.localScale.y / 2f;

                    treeBabe.transform.position = new Vector3(myPos.x + x * currentBlockType.transform.localScale.x, y + adjust, myPos.z + z * currentBlockType.transform.localScale.z);

                    treeBabe.GetComponent<Renderer>().material.color = new Color(0.6f, 0.1f, 0.12f);

                    GameObject treeHead = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    treeHead.GetComponent<Renderer>().material.color = new Color(0f, 0.6f, 0f, 0.7f);

                    treeHead.transform.localScale *= tT.y / 4f + 4f;

                    treeHead.transform.position = new Vector3(myPos.x + x * currentBlockType.transform.localScale.x , y + adjust + tT.y / 2f, myPos.z + z * currentBlockType.transform.localScale.z);

                }
            }
        }
    }
}