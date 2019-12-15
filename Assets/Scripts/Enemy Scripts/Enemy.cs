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
    [Header("State Machine")]
    public EnemyState currentState;
    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseDamage;
    public float moveSpeed;

    [Header("Death effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public void Knock(Rigidbody2D myRigdBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigdBody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigdBody, float knockTime)
    {
        if (myRigdBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigdBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigdBody.velocity = Vector2.zero;
        }
    }
}
