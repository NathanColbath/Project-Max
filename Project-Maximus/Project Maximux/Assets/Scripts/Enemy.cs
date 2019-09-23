using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private enum enemyStates
    {
        chasing,
        idle,
        attacking
    }



    private GameObject player;
    private Rigidbody body;
    public float moveSpeed = 5;
    public float attackRange = 2;
    public float chaseRange = 5;

    private float maxSpeed = 0.5f;

    private enemyStates currentState;

   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
        currentState = enemyStates.idle;
    }

    // Update is called once per frame
    void Update()
    {

        updateStates();

    }


    public void FixedUpdate()
    {

        if(currentState == enemyStates.chasing)
        {
            chasing();
        }else if (currentState == enemyStates.attacking)
        {
            attacking();
        }
        else if (currentState == enemyStates.idle)
        {
            idle();
        }

    }

    private void updateStates()
    {
        float distanceToPlayer  = (player.transform.position - transform.position).magnitude;

        if(distanceToPlayer < chaseRange && distanceToPlayer  > attackRange)
        {
            currentState = enemyStates.chasing;

        }else if (distanceToPlayer < attackRange)
        {
            currentState = enemyStates.attacking;
        }else
        {
            currentState = enemyStates.idle;
        }

    }

    private void attacking()
    {
        body.velocity = new Vector3(0, 0, 0);
    }

    private void chasing()
    {
        Vector3 playerDirection = player.transform.position - transform.position;

        body.AddForce(playerDirection.normalized * moveSpeed);
        body.velocity = playerDirection * maxSpeed;
    }

    private void idle()
    {
        body.velocity = new Vector3(0,0,0);
    }
}
