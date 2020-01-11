using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifeTime;
    private float lifeTimeCounter;
    public float magicCost;

    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    void Update()
    {
        lifeTimeCounter -= Time.deltaTime;
        if(lifeTimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetUp(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
