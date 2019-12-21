using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    public float sleepTime = 3f;
    void Start()
    {

    }

    void Update()
    {

    }

    public override void CheckDistance()
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

            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
        Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk &&
            currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        currentState = EnemyState.walk;
        animator.SetBool("attack", false);
        yield return new WaitForSeconds(sleepTime);
    }
}
