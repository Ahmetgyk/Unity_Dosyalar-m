using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ışıkalma : MonoBehaviour
{
    public float mes;
    public GameObject elfeneri;

    private void Update()
    {
        Vector3 ileri = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if(Physics.Raycast(transform.position,ileri,out hit, mes))
        {
            if(hit.distance <= mes && hit.collider.gameObject.tag == "fener")
            {
                Debug.Log(gameObject.name);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    elfeneri.SetActive(true);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
