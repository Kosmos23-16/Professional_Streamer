using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    [SerializeField] private GameObject defaultObject;
    [SerializeField] private GameObject purchasedObject;

    private const string ItemPurchasedKey = "item_purchased";

    void Start()
    {
        bool purchased = PlayerPrefs.GetInt(ItemPurchasedKey, 0) == 1;

        if (purchased)
        {
            if (defaultObject != null) defaultObject.SetActive(false);
            if (purchasedObject != null) purchasedObject.SetActive(true);
        }
        else
        {
            if (defaultObject != null) defaultObject.SetActive(true);
            if (purchasedObject != null) purchasedObject.SetActive(false);
        }
    }
}
