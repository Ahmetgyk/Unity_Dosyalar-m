using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knotrol2 : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rb;

    float turnsmothvelocty;
    public float turnsmothtime = 0.1f;
    public float Speed;

    private void Update()
    {
        float Hori = Input.GetAxisRaw("Horizontal");
        float Verti = Input.GetAxisRaw("Vertical");

        Vector3 HoriMove = transform.right * Hori;
        Vector3 VertiMove = transform.forward * Verti;
        Vector3 Move = (HoriMove + VertiMove).normalized * Speed;
        print(Move);
        Vector3 drection = new Vector3(Hori, 2, Verti);

        rb.MovePosition(transform.position + Move * Time.fixedDeltaTime);
 
        if (drection.magnitude >= 0.1)
        {
            float targetangle = Mathf.Atan2(drection.x, drection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmothvelocty, turnsmothtime);
            transform.rotation = Quaternion.Euler(0f, targetangle, 0f);
        }
    }
}
