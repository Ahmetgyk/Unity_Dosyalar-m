using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float playerdistance;
    public float awareAI = 10f;
    public float AImovespeed;
    public float damping = 6.0f;

    public Transform[] navpoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int destpoint = 0;
    public Transform goal;

    Vector3 poz;

    private void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;

        poz = new Vector3(player.position.x, transform.position.y, player.position.z);

        agent.autoBraking = false;
    }
    private void Update()
    {
        playerdistance = Vector3.Distance(transform.position, player.position);

        if (playerdistance < awareAI)
        {
            lookatplayer();
            Debug.Log("seen");
        }
        if (playerdistance < awareAI)
        {
            if (playerdistance > 2f)
            {
                chase();
            }
            else
            {
                gotonextpoint();
            }
        }
        else
        {
            if(agent.remainingDistance < 0.5f)
            {
                gotonextpoint();
            }
        }
    }
    private void lookatplayer()
    {
        transform.LookAt(poz);
    }
    private void gotonextpoint()
    {
        if(navpoint.Length == 0)
        {
               return;
        }
        agent.destination = navpoint[destpoint].position;
        destpoint = (destpoint + 1) % navpoint.Length;

        //void chase()

    }
    void chase()
    {
        transform.Translate(Vector3.forward * AImovespeed * Time.deltaTime);
    }
}
