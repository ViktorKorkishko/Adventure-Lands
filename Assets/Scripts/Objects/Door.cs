﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door vaiables")]
    public DoorType thisDoorType;
    public bool open;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if (playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
    }
}
