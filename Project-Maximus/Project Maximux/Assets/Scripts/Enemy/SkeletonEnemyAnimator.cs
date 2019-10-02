using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyAnimator : MonoBehaviour
{
    private Enemy enemyComp;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyComp = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per framea
    void Update()
    {
       

        if(enemyComp.getCurrentState() == EnemyState.idle)
        {
            animator.SetBool("idle", true);
            animator.SetBool("chasing", false);
            animator.SetBool("attacking", false);

            

        }
        else if (enemyComp.getCurrentState() == EnemyState.chasing)
        {
            animator.SetBool("idle", false);
            animator.SetBool("chasing", true);
            animator.SetBool("attacking", false);
        }
        else if (enemyComp.getCurrentState() == EnemyState.moving)
        {
            animator.SetBool("idle", false);
            animator.SetBool("chasing", true);
            animator.SetBool("attacking", false);
        }
        else if (enemyComp.getCurrentState() == EnemyState.attacking)
        {
            animator.SetBool("idle", false);
            animator.SetBool("chasing", false);
            animator.SetBool("attacking", true);
        }
    }
}
