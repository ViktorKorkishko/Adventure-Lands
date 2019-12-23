using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Sign
{
    public bool canFire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().canFire = true;
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        context.Raise();
        playerInRange = false;
        dialogBox.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
