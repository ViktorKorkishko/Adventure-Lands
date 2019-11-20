using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseDamage;
    public float moveSpeed;

    public void Knock(Rigidbody2D myRigdBody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigdBody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigdBody, float knockTime)
    {
        if(myRigdBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigdBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigdBody.velocity = Vector2.zero;
        }
    }
}
