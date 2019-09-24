using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Heath))]

public class Enemy : MonoBehaviour
{


    private enum enemyStates
    {
        chasing,
        idle,
        attacking
    }



    
    public float moveSpeed = 5;
    public float attackRange = 2;
    public float chaseRange = 5;
    public float timeBetweenAttacks = .5f;

    private float maxSpeed = 0.5f;
    private GameObject player;
    private Rigidbody body;

    private enemyStates currentState;
    private Vector3 spawnPosition;
    private Vector3 idleMovePosition;

    private bool movingToPoint = false;

    private NavMeshAgent AiAgent;

    private float timeBetweenChangingPoints;
    private float timer = 0;

    private bool countingDownTimer = false;

    
    private float attackTimer;

    private Material idleMat;
    private Material attackMat;



   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AiAgent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        currentState = enemyStates.idle;
        spawnPosition = transform.position;

        timeBetweenChangingPoints = Random.Range(1, 3);

        attackTimer = timeBetweenAttacks;

        

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
            //movingToPoint = true;
            currentState = enemyStates.idle;
        }

    }

    private void attacking()
    {
        body.velocity = new Vector3(0, 0, 0);
        attackTimer += Time.deltaTime;

        if(attackTimer >= timeBetweenAttacks)
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position + transform.forward * 2, 3);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].gameObject.CompareTag("Player"))
                {
                    Heath playerHeath = collisions[i].gameObject.GetComponent<Heath>();

                    playerHeath.reduceHeath(1);
                    Debug.Log(playerHeath.getHitPoints());
                }
            }

            attackTimer = 0;
        }


        


        

        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
    }

    private void chasing()
    {
        movingToPoint = false;
        AiAgent.SetDestination(player.transform.position);
    }

    private void idle()
    {
        if (movingToPoint)
        {
            if (distanceBetweenVectors(transform.position, idleMovePosition) < .5) {
                transform.position = idleMovePosition;
                countingDownTimer = true;
            }

            if (countingDownTimer)
            {
                timer += Time.deltaTime;
                //Debug.Log(timer);
                if (timer >= timeBetweenChangingPoints)
                {
                    movingToPoint = false;
                    timer = 0;
                    countingDownTimer = false;
                }
            }

            

         
        }else if(!movingToPoint)
        {
            idleMovePosition = randomNavmeshLocation(5);
            while (idleMovePosition == Vector3.zero)
            {
                idleMovePosition = randomNavmeshLocation(5);
            }

            
            movingToPoint = true;
            AiAgent.SetDestination(idleMovePosition);

           
            transform.rotation = Quaternion.LookRotation(idleMovePosition - transform.position);
        }

        
        
    }

    private Vector3 randomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += spawnPosition;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;
        if(NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPos = hit.position;
        }

        return finalPos;
    }

    private float distanceBetweenVectors(Vector3 start, Vector3 end)
    {
        return (start - end).magnitude;
    }
}
