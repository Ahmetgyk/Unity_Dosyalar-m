using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEney : MonoBehaviour
{
    private float turnSpeed = 4.0f;
    private Transform player;
    private Transform ax;

    private float height = 1f;
    private float distance = 2f;

    float ho = 0;
    float ve = 0;

    private Vector3 offsetX;
    private Vector3 offsetY;
    private Vector3 Vector;



    private Transform center;
    private Vector3 Xaxis = Vector3.up;
    private Vector3 Yaxis = Vector3.left;
    private float radius = 2.0f;
    private float radiusSpeed = 0.5f;
    private float rotationSpeed = 80.0f;



    public CharacterController cahr;
    public Rigidbody rb;
    public Transform cam;

    public float speed = 0.6f;
    public float turnsmothtime = 0.1f;
    float turnsmothvelocty;


    Vector2[,] matrix = new Vector2[5, 5];

    void Start()
    {
        offsetX = new Vector3(0, height, distance);
        offsetY = new Vector3(0, 0, distance);

        //transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    void LateUpdate()
    {
        GHI();
    }
    void DEF()
    {
        transform.RotateAround(center.position, Xaxis, rotationSpeed * Time.deltaTime);
        transform.RotateAround(center.position, Yaxis, Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);
        var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }
    void ABC()
    {
        ax.transform.position = Vector;

        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime, Vector3.right) * offsetY;

        transform.position = offsetX + offsetY + Vector;
        var desiredPosition = (transform.position - center.position).normalized * radius + player.position;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        transform.LookAt(player.position);

        ho += Input.GetAxis("Horizontal");
        ve += Input.GetAxis("Vertical");
        player.transform.position = new Vector3(ho, 2, ve);
    }
    void GHI()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 drection = new Vector3(hor, 2, ver);
        if (drection.magnitude >= 0.1)
        {
            float targetangle = Mathf.Atan2(drection.x, drection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmothvelocty, turnsmothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
           

            Vector3 movedrive = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            // cahr.Move(movedrive.normalized * speed * Time.deltaTime);
            rb.MovePosition(movedrive.normalized * speed * Time.deltaTime);
        }
    }
}
