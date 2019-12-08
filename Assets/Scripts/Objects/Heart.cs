﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;


    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            if(playerHealth.initialValue > heartContainers.RuntimeValue * 2)
            {
                playerHealth.initialValue = heartContainers.RuntimeValue * 2;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
