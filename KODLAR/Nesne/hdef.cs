using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hdef : MonoBehaviour
{
    public float can;
    public float zaman;
    public float maxzaman;
    public bool yerde;
    public GameObject canbar;
    private void Start()
    {
        can = 100;
    }

    private void Update()
    {
        canbar.transform.localScale = new Vector3(1, can / 100, 1);

        if (can <= 0 && !yerde)
        {
            yerde = true;
            can = 0;
            GetComponent<Animation>().Play("yerde");
        }
        if (yerde)
        {
            zaman += Time.deltaTime;

            if (zaman >= maxzaman)
            {
                yerde = false;
                can = 100;
                GetComponent<Animation>().Play("ayakta");
                zaman = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && !yerde)
        {
            can -= Random.Range(10, 15);
        }
    }
}
