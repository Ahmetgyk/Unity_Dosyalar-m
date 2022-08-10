using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    public float sensvity = 20f;
    private float RotX;
    private float RotY;

    public GameObject Player;
    public bool Move;

    private void FixedUpdate()
    {
        Move = Player.GetComponent<Kontrol>().Moving;

        RotX += Input.GetAxisRaw("Mouse X") * sensvity * Time.fixedDeltaTime * 10f;
        RotY += -Input.GetAxisRaw("Mouse Y") * sensvity * Time.fixedDeltaTime * 10f;

        RotY = Mathf.Clamp(RotY, -35, 60);
        transform.eulerAngles = new Vector3(RotY, 0f, 0f);

        Vector3 vector = new Vector3(0f, RotX, 0f);
        Vector3 vectorP = new Vector3(RotY, Player.transform.rotation.y, 0);
        Vector3 vectorM = new Vector3(RotY, RotX, 0);

        Player.transform.eulerAngles = (Move == true) ? vector : Player.transform.eulerAngles;
        if (Move)
        {
            transform.eulerAngles = Player.transform.eulerAngles;
        }
    }
}
