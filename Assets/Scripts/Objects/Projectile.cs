using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stats")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Lifetime Stats")]
    public float lifeTime;
    private float lifeTimeSeconds;
    public Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifeTimeSeconds = lifeTime;
    }

    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if(lifeTimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody.velocity = initialVelocity * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
