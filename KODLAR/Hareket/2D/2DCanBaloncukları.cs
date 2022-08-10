using UnityEngine;
using UnityEngine.UI;

public class heart : MonoBehaviour
{
    public Image[] hearts;

    public int health = 5;
    int maxHealth = 5;

    public void Damage(int amount)
    {
        hearts[health - 1].enabled = false;
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Regen(int amount)
    {
        health += amount;

        for (int i = 0; i < health; i++)
        {
            hearts[i].enabled = true;
        }
    }

    private void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (health > 0)
            {
                Damage(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (health < maxHealth)
            {
                Regen(1);
            }
        }
    }
    // karaktere yeni script oluştrup koy
    //public int damage;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //if (collision.transform.tag == "enemy")
    //{
    //collision.GetComponent<enemyhealth>().health -= damage;
    //}
    //}
}
