using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int basePrice = 1000;
    [SerializeField] private string itemID;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text priceText;

    [Header("🔹 Варіант 1: Заміна об'єкта")]
    [SerializeField] private GameObject standartItem;
    [SerializeField] private GameObject buyItem;

    private const string CoinsKey = "coins";
    private int maxLevel = 3;

    private string LevelKey => $"shop_item_{itemID}_level";

    void Start()
    {
        if (buyButton != null) buyButton.onClick.AddListener(BuyItem);
        UpdateUI();
    }

    private void BuyItem()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        int currentLevel = PlayerPrefs.GetInt(LevelKey, 0);

        if (currentLevel >= maxLevel)
        {
            Debug.Log($"{itemID} вже максимального рівня.");
            return;
        }

        int price = GetPrice(currentLevel);

        if (currentCoins >= price)
        {
            currentCoins -= price;
            PlayerPrefs.SetInt(CoinsKey, currentCoins);

            currentLevel++;
            PlayerPrefs.SetInt(LevelKey, currentLevel);
            PlayerPrefs.Save();

            FindObjectOfType<CoinsDisplay>()?.RefreshCoinsUI();

            Debug.Log($"{itemID} прокачаний до {currentLevel} рівня за {price} монет.");
        }
        else
        {
            Debug.Log("Недостатньо монет.");
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        int currentLevel = PlayerPrefs.GetInt(LevelKey, 0);

        if (currentLevel >= maxLevel)
        {
            if (priceText != null) priceText.text = "Max Level";
            if (buyButton != null) buyButton.interactable = false;

            if (standartItem != null) standartItem.SetActive(false);
            if (buyItem != null) buyItem.SetActive(true);

            return;
        }

        int price = GetPrice(currentLevel);
        if (priceText != null) priceText.text = $"{price} $";

        if (buyButton != null) buyButton.interactable = (currentCoins >= price);

        if (currentLevel > 0)
        {
            if (standartItem != null) standartItem.SetActive(false);
            if (buyItem != null) buyItem.SetActive(true);
        }
        else
        {
            if (standartItem != null) standartItem.SetActive(true);
            if (buyItem != null) buyItem.SetActive(false);
        }
    }

    private int GetPrice(int level)
    {
        return basePrice + (level * 1000);
    }
}
