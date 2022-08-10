using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron: MonoBehaviour
{
    public float speed;

    public Transform drone;
    public float mes;
    private float time = 0;
    public int damage;

    public GameObject Enemy;

    public bool engel;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engel = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Atilemap")
        {
            engel = true;
        }

        if (collision.transform.tag == "enemy")
        {
            Enemy = GameObject.FindGameObjectWithTag("enemy");
            Enemy.GetComponent<Enemy1>().Takedamage(damage);
        }
    }
    private void FixedUpdate()
    {
        mes = Vector3.Distance(transform.position, drone.position);
        transform.position = Vector2.MoveTowards(transform.position, drone.position, speed * Time.deltaTime);

        if (engel)
        {
            rb.AddForce(Vector2.up * 5);

            time += Time.deltaTime;

            if(time > 1f)
            {
                rb.AddForce(Vector2.down * 5);
                engel = false;
            }
        }
    }


}
