using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : PowerUp
{
    public FloatValue heartContainer;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            heartContainer.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainer.RuntimeValue * 2;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
