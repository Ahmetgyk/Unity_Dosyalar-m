using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    [Header("ana kamera")]
    public Camera maincamera;

    [Header("dürbün resmi")]
    public Image binocularsoverlay;

    [Header("normal fov - zoom fov")]
    public float normalfov;
    public float zoomfov;

    public Slider slider;

    bool isusing;
    bool isequipped;

    private void Update()
    {
        float zoomfov = slider.value * 100;

        if (Input.GetKeyDown(KeyCode.B))
        {
            isequipped = !isequipped;
            checkstate();
        }
        if (isequipped)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                maincamera.fieldOfView = zoomfov;

                //maincamera.depth = 5;
            }
            else
            {
                maincamera.fieldOfView = normalfov;
            }
        }
    }
    void checkstate()
    {
        if (isequipped)
        {
            binocularsoverlay.enabled = true;
        }
        else
        {
            binocularsoverlay.enabled = false;
        }
    }
}
