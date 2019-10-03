using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Heath))]

public class Enemy : MonoBehaviour
{





    
    public float moveSpeed = 5;
    public float attackRange = 2;
    public float chaseRange = 5;
    public float timeBetweenAttacks = 48 * 1.16f;
    public float randomMovePositionRadius = 5;
    public bool isMinnion;
    public float clippingRange = .5f;
    public GameObject followTarget;


    private float maxSpeed = 0.5f;
    private GameObject player;
    //private Rigidbody body;
    private float lerpSpeed = .25f;

    private EnemyState currentState;
    private Vector3 spawnPosition;
    private Vector3 idleMovePosition;

    private bool movingToPoint = false;

    private NavMeshAgent AiAgent;

    public float timeBetweenChangingPoints = 1;
    private float timer = 0;

    private bool countingDownTimer = false;

    
    private float attackTimer;

    private Material idleMat;
    private Material attackMat;




    private bool stopped;


   

    // Start is called before the first frame update
    void Start()
    {
     
        player = GameObject.FindGameObjectWithTag("Player");
        AiAgent = GetComponent<NavMeshAgent>();
   
        currentState = EnemyState.idle;
        spawnPosition = transform.position;

        //timeBetweenChangingPoints = Random.Range(1, 2);

      
        

        
        

        

        attackTimer = timeBetweenAttacks;

        

        AiAgent.speed = moveSpeed;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        updateStates();

        if (isMinnion && followTarget != null)
        {
            spawnPosition = followTarget.transform.position;
        }

        

    }


    public void FixedUpdate()
    {

        if(currentState == EnemyState.chasing)
        {
            chasing();
            
        }else if (currentState == EnemyState.attacking)
        {
            attacking();

        }
        else if (currentState == EnemyState.idle)
        {
            idle();
        }



    }

    private void updateStates()
    {
        float distanceToPlayer  = distanceBetweenVectors(player.transform.position, transform.position);

        if (distanceToPlayer < chaseRange && distanceToPlayer  > attackRange)
        {
            currentState = EnemyState.chasing;

        }
        if (distanceToPlayer < attackRange && distanceToPlayer < chaseRange)
        {
            stopped = false;
            currentState = EnemyState.attacking;
        }else if(distanceToPlayer > chaseRange)
        {
            //movingToPoint = true;
            currentState = EnemyState.idle;
        }

        

    }

    private void lerpRotation()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, lerpSpeed);
    }
    
    
    //Function used to attak the player and reduce its health below
    //TODO fix it so that the attack will be on time with the animations
    private void attacking()
    {

        if (!stopped)
        {
            AiAgent.SetDestination(transform.position);
            stopped = true;
        }


        lerpRotation();
        attackTimer += Time.deltaTime;

            
            

           
        
    
    }

    public void attack()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position + (transform.forward), attackRange);
        for (int i = 0; i < collisions.Length; i++)
        {


            if (collisions[i].gameObject.CompareTag("Player"))
            {
                Heath playerHeath = collisions[i].gameObject.GetComponent<Heath>();

                playerHeath.reduceHeath(1);
                //Debug.Log(playerHeath.getHitPoints());
            }
        }
    }
    
    //Function used to set the AiAgent and set its destination to the player
    private void chasing()
    {
        movingToPoint = false;
        lerpRotation();
        AiAgent.SetDestination(player.transform.position);
        
    }

    //Function used to idle the enemy and move the 
    //enemy to random points on the map
    private void idle()
    {
        if (movingToPoint)
        {
            currentState = EnemyState.moving;
            if (distanceBetweenVectors(transform.position, idleMovePosition) < clippingRange) {
                transform.position = idleMovePosition;
                countingDownTimer = true;
                
            }else
            {
                
            }

            if (countingDownTimer)
            {
                currentState = EnemyState.idle;
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
                idleMovePosition = randomNavmeshLocation(randomMovePositionRadius);
            }

            
            movingToPoint = true;
            AiAgent.SetDestination(idleMovePosition);
            currentState = EnemyState.moving;



        }

        
        //Debug.Log(distanceBetweenVectors(transform.position, idleMovePosition));
        
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

    public EnemyState getCurrentState()
    {
        return currentState;
    }
}
