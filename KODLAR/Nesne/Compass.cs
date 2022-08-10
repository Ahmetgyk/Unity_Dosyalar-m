using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconpr;
    List<Compassİmage> compim = new List<Compassİmage>();

    public RawImage compass;
    public Transform player;

    float compassUnit;
    public float maxdistance = 200f;

    public Compassİmage one;
    public Compassİmage two;
    public Compassİmage three;

    private void Start()
    {
        compassUnit = compass.rectTransform.rect.width / 360;

        addquestmarker(one);
        addquestmarker(two);
        addquestmarker(three);
    }
    private void Update()
    {
        compass.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(Compassİmage marker in compim)
        {
            marker.image.rectTransform.anchoredPosition = Getposoncompass(marker);

            float dst = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
            float scale = 0f;
            if (dst < maxdistance)
                scale = 1f - (dst / maxdistance);
            // marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }
    public void addquestmarker(Compassİmage marker)
    {
        GameObject newmarker = Instantiate(iconpr, compass.transform);
        marker.image = newmarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        compim.Add(marker);
    }
    Vector2 Getposoncompass(Compassİmage marker)
    {
        Vector2 playerpos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerfwd = new Vector2(player.transform.forward.x, player.transform.forward.z);
        float angle = Vector2.SignedAngle(marker.position - playerpos, playerfwd);
        return new Vector2(compassUnit * angle, 0f);
    }
}
