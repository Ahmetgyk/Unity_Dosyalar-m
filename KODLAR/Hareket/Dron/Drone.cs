using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    private float speed;
    public float walkspeed;
    public float runspeed;
    public float mes;
    public float force;
    public float jumpforce;
    public float sensvity;
    public bool control;
    private float time;

    public Transform trigger;
    public Transform player;
    public Camera cam;
    public Image image;

    Vector3 poz;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = walkspeed;
        control = false;
    }
    private void FixedUpdate()
    {
        mes = Vector3.Distance(transform.position, trigger.position);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            control = !control;
        }
        if (control)
        {
            cam.depth = 2;
            image.enabled = true;
            Control();
            Look();
        }
        else if (!control)
        {
            cam.depth = 0;
            image.enabled = false;
            Move();
        }
    }
    private void Move()
    {
        if (mes <= 5)
        {
            speed = walkspeed;
        }
        if (mes > 5)
        {
            speed = runspeed;
        }

        transform.position = Vector3.MoveTowards(transform.position, trigger.position, speed * Time.deltaTime);
        poz = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(poz);
    }
    private void Control()
    {
        float XMov = Input.GetAxisRaw("Horizontal");
        float ZMov = Input.GetAxisRaw("Vertical");
        Vector3 movehor = transform.right * XMov;
        Vector3 movever = transform.forward * ZMov;
        Vector3 move = (movehor + movever).normalized * force;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpforce);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(Vector3.down * jumpforce);
        }
    }
    public void Look()
    {
        float YRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, YRot, 0f) * sensvity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

    }

}
