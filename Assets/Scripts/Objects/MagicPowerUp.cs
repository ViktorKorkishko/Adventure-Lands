using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerUp : PowerUp
{
    public Inventory playerInventory;
    public float magicValue;

    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            playerInventory.currentMagic += magicValue;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
