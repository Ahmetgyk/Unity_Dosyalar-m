using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector3 offset;
    public int damage;
    RaycastHit2D hit;
    bool canattack = true;
    bool faceright = true;
    Vector2 forward;


    private void attack()
    {
        canattack = false;
        if (!faceright)
        {
            forward = transform.TransformDirection(Vector2.right * -2);
        }
        else
        {
            forward = transform.TransformDirection(Vector2.right * 2);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, forward, 1.0f);
        if (hit)
        {
            if (hit.transform.tag == "emeny")
            {
                //"enemyhealth" adlı bir script oluştur 
                hit.transform.GetComponent<enemyhealth>().GetDamage(damage);
            }
            else
            {
                Debug.Log("nothing to hit");
            }
        }
        Animator.SetTrigger("attack");

        StartCoroutine(attackDelay());
    }
    IEnumerable attackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canattack = true;
    }
}