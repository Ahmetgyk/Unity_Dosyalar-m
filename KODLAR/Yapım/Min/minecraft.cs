
using UnityEngine;

public class minecraft : MonoBehaviour
{
    //public GameObject[] blocks;

    public GameObject block;
    public GameObject previevbolock;

    int rotY = 0;
    Vector3 spawnrot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //block = blocks[0];
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        spawnrot.y = rotY;

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
            // selectblock(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
           // selectblock(1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
           // selectblock(2);
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotY -= 90;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotY += 90;
        }

        if (Physics.Raycast(ray,out hit, 5))
        {
            if (hit.transform.tag == "Buildable")
            {
                Vector3 prevpos = hit.point;
                previevbolock.transform.position = new Vector3(Mathf.Round(prevpos.x), Mathf.Round(prevpos.y), Mathf.Round(prevpos.z));
                previevbolock.transform.localEulerAngles = spawnrot;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject Build = Instantiate(block, transform.position, transform.rotation);
                    Vector3 buildpos = hit.point;

                    //Build.transform.rotation = new Quaternion(0, 0, 0, 0);
                    Build.transform.localEulerAngles = spawnrot;
                    Build.transform.position = new Vector3(Mathf.Round(buildpos.x), Mathf.Round(buildpos.y), Mathf.Round(buildpos.z));
                }

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
    //public void selectblock(int selection)
    //{
        //previevbolock.SetActive(false);
        //block = blocks[selection];
    //}
}
