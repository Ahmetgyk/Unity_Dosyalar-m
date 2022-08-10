using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public GameObject mesafe;
    public int armor, health, damage;
    public float speed;
    public bool faceright, turn;
    public Transform[] pos;
    public Transform player;
    public float mesP, mes;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        faceright = true;
        turn = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<heart>().Takedamage(damage);
            anim.SetBool("attack1", true);
        }
        else
        {
            anim.SetBool("attack1", false);
        }
    }
    private void FixedUpdate()
    {
        mes = Vector3.Distance(transform.position, player.position);

        anim.SetBool("run", true);
        if (turn)
        {
            if (faceright)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos[1].position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, pos[1].position) < 0.5f)
                {
                    faceright = !faceright;
                    Vector3 scaler = transform.localScale;
                    scaler.x *= -1;
                    transform.localScale = scaler;

                    anim.SetBool("run", true);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, pos[0].position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, pos[0].position) < 0.5f)
                {
                    faceright = !faceright;
                    Vector3 scaler = transform.localScale;
                    scaler.x *= -1;
                    transform.localScale = scaler;

                    anim.SetBool("run", true);
                }
            }
        }

        if (mes < 13 && mes > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            turn = false;
            if (mes < mesP && !faceright)
            {
                faceright = !faceright;
                Vector3 scaler = transform.localScale;
                scaler.x *= -1;
                transform.localScale = scaler;
            }
            if (mesP < mes && faceright)
            {
                faceright = !faceright;
                Vector3 scaler = transform.localScale;
                scaler.x *= -1;
                transform.localScale = scaler;
            }
        }
        else
        {
            turn = true;
        }
    }
    private void flip()
    {
        faceright = !faceright;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        speed *= -1;
        anim.SetBool("run", true);
    }
    public void Takedamage(int amount)
    {
        rb.AddForce(Vector2.right * 300);

        if (armor <= 0)
        {
            armor = 0;

        }
        if (armor >= amount)
        {
            armor -= amount;
        }
        else
        {
            armor -= amount;
            health += armor;
        }

        anim.SetBool("hit", true);

        if (health <= 0)
        {
            anim.SetBool("dead", true);
        }


    }
    public void nothit()
    {
        anim.SetBool("hit", false);
    }
    public void end()
    {
        Destroy(gameObject);
    }
    public void notattack1()
    {
        anim.SetBool("attack1", false);
    }
}
 