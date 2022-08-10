using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensvity;
    float XClamp = 0;

    public bool control;

    public Transform player;

    private void Start()
    {
        control = true;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            control = !control;
        }
        if (control)
        {
            look();
        }
    }
    private void look()
    {
        float XRot = Input.GetAxisRaw("Mouse X") * sensvity * Time.deltaTime;
        float YRot = Input.GetAxisRaw("Mouse Y") * sensvity * Time.deltaTime;

        XClamp -= YRot;
        XClamp = Mathf.Clamp(XClamp,-70f, 40f);

        transform.localRotation = Quaternion.Euler(XClamp, 0f, 0f);
        player.Rotate(Vector3.up * XRot);
    }
}
