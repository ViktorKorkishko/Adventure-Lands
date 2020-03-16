using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    public FloatValue playerMagic;
    public mySignal magicSignal;

    public void Use(int amountToIncrease)
    {
        playerMagic.RuntimeValue += amountToIncrease;
        magicSignal.Raise();
    }
}
