using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int itemPrice;
    [SerializeField] private string itemID;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text priceText;

    [Header("🔹 Варіант 1: Заміна об'єкта")]
    [SerializeField] private GameObject standartItem;
    [SerializeField] private GameObject buyItem;

    private const string CoinsKey = "coins";
    private string PurchasedKey => $"shop_item_{itemID}_purchased";

    void Start()
    {
        if (priceText != null) priceText.text = itemPrice + " $";
        if (buyButton != null) buyButton.onClick.AddListener(BuyItem);

        if (IsPurchased())
        {
            MarkAsPurchased();
            ActivatePurchasedItem();
        }
        else
        {
            if (standartItem != null) standartItem.SetActive(true);
            if (buyItem != null) buyItem.SetActive(false);
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

            MarkAsPurchased();
            ActivatePurchasedItem();

            Debug.Log($"{itemID} куплений за {itemPrice} монет.");
        }
        else
        {
            Debug.Log("Недостатньо монет або предмет вже куплений.");
        }
    }

    private bool IsPurchased()
    {
        return PlayerPrefs.GetInt(PurchasedKey, 0) == 1;
    }

    private void MarkAsPurchased()
    {
        if (buyButton != null) buyButton.interactable = false;
        if (priceText != null) priceText.text = "Purchased";
    }

    private void ActivatePurchasedItem()
    {

        if (standartItem != null) standartItem.SetActive(false);
        if (buyItem != null) buyItem.SetActive(true);
    }
}
