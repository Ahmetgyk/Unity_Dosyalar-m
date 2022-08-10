using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ışık : MonoBehaviour
{
    public Light isik;
    public bool acik;

    public AudioSource source;
    public AudioClip fenerses;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = fenerses;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            acik = !acik;
            source.Play();
        }
        if (acik)
        {
            isik.enabled = true;
        }
        else
        {
            isik.enabled = false;
        }
    }
}
