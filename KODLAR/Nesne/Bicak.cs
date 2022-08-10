using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicak : MonoBehaviour
{
    public float speed;
    public float sensvity;
    private bool Firlatildi;
    private bool Con;

    Rigidbody Rb;
    public GameObject Effeck;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != null)
        {
            Con = false;
        }
    }
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Firlatildi = false;
        Con = true;
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Firlatildi = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(Effeck, transform.position, transform.rotation);
        }

        if (Firlatildi && Con)
        {
            float Hori = Input.GetAxis("Mouse X") * sensvity * Time.fixedDeltaTime;
            transform.Rotate(0, 0, -Hori);

            float Move = speed * Time.fixedDeltaTime;
            Vector3 MoveHori = (transform.up * Move).normalized;
            Rb.MovePosition(Rb.position + MoveHori);
        }
    }
}
