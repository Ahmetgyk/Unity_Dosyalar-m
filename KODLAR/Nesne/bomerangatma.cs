using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomerangatma : MonoBehaviour
{
    public GameObject bumerang;
    public GameObject bitis;
    public Transform cikis;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bumerang, cikis.transform.position, Quaternion.LookRotation(transform.forward));
            Instantiate(bitis, cikis.transform.position, Quaternion.LookRotation(transform.forward));
        }
    }
}
