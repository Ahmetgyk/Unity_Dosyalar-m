using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duvar : MonoBehaviour
{
    public Transform Sur1;
    public Transform Sur2;

    private float Uzunluk;
    private bool yoket;
    private bool rig;

    private void Start()
    {
        yoket = true;
        rig = true;
    }

    private void Update()
    {
        if (Sur1 != null && Sur2 != null)
        {
            Uzunluk = Vector3.Distance(Sur1.position, Sur2.position);

            float x = (Mathf.Max(Sur1.position.x, Sur2.position.x) - Mathf.Min(Sur1.position.x, Sur2.position.x)) / 2 + Mathf.Min(Sur1.position.x, Sur2.position.x);
            float z = (Mathf.Max(Sur1.position.z, Sur2.position.z) - Mathf.Min(Sur1.position.z, Sur2.position.z)) / 2 + Mathf.Min(Sur1.position.z, Sur2.position.z);
            transform.position = new Vector3(x, 1, z);

            transform.localScale = new Vector3(1, 3, Uzunluk);
            this.gameObject.transform.GetChild(0).transform.position = Sur1.position;
            Vector3 poz = new Vector3(Sur1.position.x, transform.position.y, Sur1.position.z);
            transform.LookAt(poz);
        }
        else
        {
            Destroy(gameObject);
        }

        if (rig)
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
            rig = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bina"))
        {
            Destroy(collision.gameObject);
        }
    }
}
