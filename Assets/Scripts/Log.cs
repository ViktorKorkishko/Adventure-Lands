using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    // public Transform homePosition;
    public Animator animator;

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }
    
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius &&
        Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle ||
            currentState == EnemyState.walk &&
            currentState != EnemyState.stagger)
            {
                animator.SetBool("wakeUp", true);
                Invoke("MoveLog", 1f);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }

    private void MoveLog()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius &&
        Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle ||
            currentState == EnemyState.walk &&
            currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
    }

    private void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
