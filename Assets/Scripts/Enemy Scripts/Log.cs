using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Rigidbody2D myRigidBody;
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
        animator.SetBool("wakeUp", true);
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
        Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle ||
            currentState == EnemyState.walk &&
            currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }

public void ChangeAnim(Vector2 direction)
{
    direction = direction.normalized;
    animator.SetFloat("horizontal", direction.x);
    animator.SetFloat("vertical", direction.y);
}

private void ChangeState(EnemyState newState)
{
    if (currentState != newState)
    {
        currentState = newState;
    }
}
}
