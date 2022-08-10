using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float smoothx;
    [SerializeField]
    private float smoothy;
    [SerializeField]
    private float mixX;
    [SerializeField]
    private float mixY;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float maxY;
    void Start()
    {
        player = GameObject.Find("player").transform;

    }

    private void LateUpdate()
    {
        float posX = Mathf.MoveTowards(transform.position.x, player.position.x, smoothx);
        float posY = Mathf.MoveTowards(transform.position.y, player.position.y, smoothy);

        transform.position = new Vector3(Mathf.Clamp(posX, mixX, maxX), Mathf.Clamp(posY, mixY, maxY), transform.position.z);

    }
}
