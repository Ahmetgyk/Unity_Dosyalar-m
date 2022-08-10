using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Kontrol : MonoBehaviour
{
    public float WalkSpeed = 10f;
    public float RunSpeed = 20;
    public float JumpSpeed = 10;
    public AnimationCurve ForceR;
    public AnimationCurve ForceJ;
    public float Lerptime;
    private float Timer = 0.1f;
    private float force;
    private float Speed;

    public bool CanJump;
    public bool Moving;


    public float turnsmothtime = 0.1f;
    float turnsmothvelocty;
    public Transform cam;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CanJump = true;
        Speed = WalkSpeed;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            if (Timer > Lerptime)
            {
                Timer = Lerptime;
            }
            force = Timer / Lerptime;
            force = 1 / Time.fixedDeltaTime;
            rb.AddForce(Vector3.up * JumpSpeed * ForceJ.Evaluate(force) * 200);
            CanJump = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Timer = 0f;
            force = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && CanJump)
        {
            Timer += Time.fixedDeltaTime;
            if (Timer > Lerptime)
            {
                Timer = Lerptime;
            }
            force = Timer / Lerptime;

            Speed = RunSpeed*ForceR.Evaluate(force) + 5f;

        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = WalkSpeed;
            Timer = 0f;
            force = 0f;
        }

        float Hori =  Input.GetAxisRaw("Horizontal");
        float Verti = Input.GetAxisRaw("Vertical");

        Vector3 HoriMove = transform.right * Hori;
        Vector3 VertiMove = transform.forward * Verti;
        Vector3 Move = (HoriMove + VertiMove).normalized * Speed;
        Vector3 drection = new Vector3(Hori, 2, Verti);

        rb.MovePosition(transform.position + Move * Time.fixedDeltaTime);
        if (drection.magnitude >= 0.1)
        {
            float targetangle = Mathf.Atan2(drection.x, drection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmothvelocty, turnsmothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //  do
        // {
        // }
        // while (CanJump);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Zemin")
        {
            CanJump = true;
        }
    }
}
