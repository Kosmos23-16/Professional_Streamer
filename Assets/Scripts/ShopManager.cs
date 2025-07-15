using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    private const string CoinsKey = "coins";
    private const string ItemPurchasedKey = "item_purchased";
    private int itemCost = 5000;

    void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
        if (PlayerPrefs.GetInt(ItemPurchasedKey, 0) == 1)
        {
            buyButton.interactable = false;
        }
    }

    private void BuyItem()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey, 0);

        if (coins >= itemCost)
        {
            coins -= itemCost;
            PlayerPrefs.SetInt(CoinsKey, coins);
            PlayerPrefs.SetInt(ItemPurchasedKey, 1);
            PlayerPrefs.Save();

            buyButton.interactable = false;

           
        }
    }
}
