using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binaayarları : MonoBehaviour
{
    public string Name;
    public Collider na;

    public void Durum()
    {
        gameObject.name = Name;
        Collider[] binacoller = Physics.OverlapSphere(transform.position, 0.75f);
        foreach (Collider binacol in binacoller)
        {
            if (binacol.gameObject.tag.Equals("Bina"))
            {
                if (binacol.name != Name)
                {
                    na = binacol;
                }
            }
        }
        if (na == null)
        {
            Destroy(gameObject);
        }
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
