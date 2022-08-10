using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float countdown;

    bool Boom = false;

    public GameObject effeck;

    private void Start()
    {
        countdown = delay;
    }
    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !Boom)
        {
            Explode();
            Boom = true;
        }
    }
    void Explode()
    {
        Instantiate(effeck, transform.position, transform.rotation);
        Collider[] colliderstodestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyobject in colliderstodestroy)
        {

            //parcalanma kodu
            //Script.. scr = nearbyobject.GetComponent < Script..> ();
            //if(scr != null)
            //{
            //scr.destroy();
            //}
        }

        Collider[] colliderstomove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyobject in colliderstomove)
        {
            Rigidbody rb = nearbyobject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
