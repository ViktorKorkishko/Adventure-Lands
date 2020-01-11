using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    void Start()
    {
        coinDisplay.text = playerInventory.coins.ToString();
    }

    public void UpdateCoins()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
