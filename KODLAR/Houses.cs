using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houses : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Road" || col.gameObject.tag == "House")
        {
            Destroy(gameObject);
        }
    }

}
