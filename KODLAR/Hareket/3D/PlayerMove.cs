using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float speed;
    public float walkspeed;
    public float runspeed;
    public float Jumpforce;
    private bool jump;
    private bool Sürünme;
    private bool Kayma;
    public float basmasayisi;
    private Vector3 shootposition;

    public Camera cam;
    public Image image;
    public Transform ornek;
    private Animator anim;

    Rigidbody rb;
    private Control control;

    private enum Control
    {
        PlayerControl,
        DroneControl,
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        jump = true;
        Sürünme = false;
        Kayma = false;
        control = Control.PlayerControl;

    }
    private void FixedUpdate()
    {
        switch (control)
        {
            default:
            case Control.PlayerControl:
                Move();
                Jump();
                Down();
                cam.depth = 2;
                image.enabled = true;
                break;
            case Control.DroneControl:
                cam.depth = 0;
                image.enabled = false;

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    control = Control.PlayerControl;
                }
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Zemin")
        {
            jump = true;
        }
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runspeed;

            if (Input.GetKey(KeyCode.LeftControl) && !Sürünme)
            {

                anim.SetBool("cömelme", true);
                Kayma = true;

                Vector3 fow = transform.forward;
                rb.AddForce(fow * 30);
            }
            else
            {
                anim.SetBool("cömelme", false);
                Kayma = false;
            }
        }
        else
        {
            speed = walkspeed;
        }

        float Hori = Input.GetAxisRaw("Horizontal");
        float Verti = Input.GetAxisRaw("Vertical");

        Vector3 MoveHori = transform.right * Hori;
        Vector3 MoveVerti = transform.forward * Verti;
        Vector3 MovePlayer = (MoveHori + MoveVerti).normalized * speed;

        rb.MovePosition(rb.position + MovePlayer * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            control = Control.DroneControl;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jump && !Sürünme)
        {
            rb.AddForce(Vector3.up * Jumpforce);
            jump = false;
        }
    }
    private void Down()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !Kayma)
        {
            basmasayisi++;
        }

        if (basmasayisi == 1f)
        {
            anim.SetBool("cömelme", true);

        }
        if (basmasayisi == 2f)
        {
            
            anim.SetBool("sürünme", true);
            anim.SetBool("cömelme", false);
            Sürünme = true; 
        }
        else if(basmasayisi == 3f)
        {
            basmasayisi = 0;
            anim.SetBool("sürünme", false);
            Sürünme = false;
        }
    }
    public void hookjump()
    {
        rb.AddForce(Vector3.up * 100f);
    }
    public void hookdown()
    {
        rb.AddForce(Vector3.down * 100f);
    }
}
