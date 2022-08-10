using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomerang : MonoBehaviour
{
    float time;
    public float width = 4;
    public float speed = 5;
    public float height = 7;
    public float rotatespeed;
    public float mes, meshit;

    Rigidbody rb;
    private GameObject bitis;
    private GameObject cam;


    public float X, Y, Z;
    float bitisX, bitisY, bitisZ;

    private void Start()
    {
        bitis = GameObject.FindGameObjectWithTag("Bitis");
        cam = GameObject.FindGameObjectWithTag("Cam");
        rb = GetComponent<Rigidbody>();

        bitisX = bitis.transform.position.x;
        bitisY = bitis.transform.position.y;
        bitisZ = bitis.transform.position.z;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            meshit = Vector3.Distance(cam.transform.position, hit.transform.position);

            if(meshit >= 40f)
            {
                meshit = 40f;
            }

            height = meshit / 2 - 1f;
            Z = meshit / 2 - 1f;
        }

    }
    private void Update()
    {
        mes = Vector3.Distance(transform.position, bitis.transform.position);

        Bomerang1();
    }
    void Bomerang1()
    {
        time += Time.deltaTime * speed;

        float xr = Mathf.Cos(time) * width;
        float yr = 0;
        float zr = Mathf.Sin(time) * height;

        transform.position = new Vector3(xr + X + bitisX , yr + Y + bitisY , zr + Z + bitisZ);

        transform.Rotate(0, -rotatespeed, 0);

        if (mes <= 0.3f)
        {
            Destroy(gameObject);
            Destroy(bitis);
        }
    }
}
