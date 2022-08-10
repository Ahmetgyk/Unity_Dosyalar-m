using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecegörüş : MonoBehaviour
{
    public GameObject gecegorus;

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            gecegorus.SetActive(true);
        }
        else
        {
            gecegorus.SetActive(false);
        }
    }
}
