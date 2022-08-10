
using UnityEngine;

public class h : MonoBehaviour
{
    // CharacterController için ilerleme ve dönme hızları
    public float speed;
    public float rotateSpeed;

    private int can;


    // Use this for initialization
    void Start()
    {
        can = 3;
    }

    // Her frame (kare) gösterilmeden önce çağırılan fonksiyon
    void Update()
    {
        // Karakteri hareket ettirme kısmı
        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

    }

    // isTrigger = true olduğu için, içine başka obje girince çalışır
    void OnTriggerEnter(Collider other)
    {

        // Çarpışmanın sahibi olan çarpan nesnenin tag'i Dusman ise
        if (other.CompareTag("Dusman"))
        {
            can = can - 1;
            if (can <= 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
