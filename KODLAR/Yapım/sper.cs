using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sper : MonoBehaviour
{
    public Transform player;
    public Transform pos;
    public float i;
    private float deger;

    void Update()
    {
        Küp();
    }
    private void Silindir()
    {
        Vector3 poz = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.LookAt(poz);
    }
    private void Küp()
    {
        Vector3 poz = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(poz);

        Vector3 playerpos = new Vector3(player.position.x, player.position.y, player.position.z);
        Vector3 sperrpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float AngleBetweenVector2(Vector3 vec1, Vector3 vec2)
        {
            Vector3 diference = vec2 - vec1;
            float sign = (vec2.z < vec1.z) ? -1.0f : 1.0f;
            deger = Vector3.Angle(Vector3.right, diference) * sign;
            return deger;
        }
        AngleBetweenVector2(playerpos, sperrpos);

        if (deger >= -45 && deger <= 45)
        {
            i = 1;
            pos.localRotation = Quaternion.Euler(0, -90, 0);
        }
        else if (deger >= 45 && deger <= 135)
        {
            i = 2;
            pos.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (deger >= 135 && deger <= 180 || deger >= -180 && deger <= -135)
        {
            i = 3;
            pos.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (deger >= -135 && deger <= -45)
        {
            i = 4;
            pos.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
