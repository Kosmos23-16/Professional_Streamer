using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int itemPrice;
    [SerializeField] private string itemID;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text priceText;

    private const string CoinsKey = "coins";
    private string PurchasedKey => $"shop_item_{itemID}_purchased";

    void Start()
    {
        priceText.text = itemPrice + " $";
        buyButton.onClick.AddListener(BuyItem);

        if (IsPurchased())
        {
            MarkAsPurchased();
        }
    }

    private void BuyItem()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);

        if (currentCoins >= itemPrice && !IsPurchased())
        {

            currentCoins -= itemPrice;
            PlayerPrefs.SetInt(CoinsKey, currentCoins);

  
            PlayerPrefs.SetInt(PurchasedKey, 1);
            PlayerPrefs.Save();

   
            FindObjectOfType<CoinsDisplay>()?.RefreshCoinsUI();


            BuffManager.Instance?.ApplyBuff(itemID);

            MarkAsPurchased();

            Debug.Log($"{itemID} куплено за {itemPrice} монет.");
        }
        else
        {
            Debug.Log("Недостатньо монет або предмет вже куплено.");
        }
    }

    private bool IsPurchased()
    {
        return PlayerPrefs.GetInt(PurchasedKey, 0) == 1;
    }

    private void MarkAsPurchased()
    {
        buyButton.interactable = false;
        priceText.text = "Purchased";
    }
}
