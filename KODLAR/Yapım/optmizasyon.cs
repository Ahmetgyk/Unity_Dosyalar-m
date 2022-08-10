using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optmizasyon : MonoBehaviour
{
    void FixedUpdate()
    {
        Collider[] nesneler = Physics.OverlapSphere(transform.position, 30f);
        
        foreach(Collider nesne in nesneler)
        {
            nesneyoketme yoketme = nesne.GetComponent<nesneyoketme>();
            if(yoketme != null)
            {
                yoketme.SET();
            }
        }
    }
}
//Cisme atanacak kod
public class nesneyoketme : MonoBehaviour
{
    MeshRenderer mesh;
    public Transform player;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        float dist;
        dist = Vector3.Distance(transform.position, player.position);

        if (dist >= 30.0f)
        {
            mesh.enabled = false;
        }
    }
    public void SET()
    {
        mesh.enabled = true;
    }
}
