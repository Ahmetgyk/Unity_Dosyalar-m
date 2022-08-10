using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dronefire : MonoBehaviour
{
    public float damage;
    public float range;
    public float timearange;
    private float timetofire;
    public float inspectorforce;

    public float ammo;
    public float ammodepost;
    public float ammomax;
    bool canfire;

    public bool control;

    public Text ammotext;
    public Camera cam;

    public ParticleSystem partical;
    public GameObject inspector;

    private void Start()
    {
        canfire = true;
        control = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            control = !control;
        }
        if (control)
        {
            ammotext.enabled = true;
            Control();
        }
        else if (!control)
        {
            ammotext.enabled = false;
        }
    }
    private void Control()
    {
        ammotext.text = ammo + "/" + ammodepost;

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= timetofire && canfire)
        {
            timetofire = Time.time + 1f / timearange;
            partical.Play();
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
    private void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            canfire = true;
        }
        else if (ammo <= 0)
        {
            canfire = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * inspectorforce);
            }

            GameObject inspectorGO = Instantiate(inspector, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(inspectorGO, 2f);
        }
    }
    IEnumerator Reload()
    {
        if (ammodepost == 0)
        {
            Debug.Log("No ammo left in dhe deposit");
            yield break;
        }
        else
        {
            canfire = false;
            Debug.Log("Reloading...");
        }

        yield return new WaitForSeconds(2f);
        canfire = true;

        float ammotoadd;
        ammotoadd = ammomax - ammo;

        if (ammodepost > ammotoadd)
        {
            ammodepost -= ammotoadd;
            ammo += ammotoadd;
        }
        else if (ammodepost <= ammotoadd)
        {
            ammo += ammodepost;
            ammodepost = 0;
        }
    }
}
