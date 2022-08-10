using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaAtma : MonoBehaviour
{
    public GameObject Bomba;
    public Transform BombaCikis;
    public float Force;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Bombaatma();
        }
    }
    private void Bombaatma()
    {
        GameObject Go = Instantiate(Bomba, BombaCikis.transform.position, BombaCikis.transform.rotation);
        Rigidbody Rb = Go.GetComponent<Rigidbody>();
        Rb.AddForce(BombaCikis.transform.forward * Force, ForceMode.VelocityChange);
    }
}
