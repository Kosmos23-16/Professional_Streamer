using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(200)] // цей скрипт спрацює ПІСЛЯ більшості інших Start
[DisallowMultipleComponent]
public class Shop : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private int itemPrice = 0;
    [SerializeField] private string itemID = "mouse";

    [Header("UI")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Text priceText;

    [Header("Модельки")]
    [SerializeField] private GameObject standartItem; // 0
    [SerializeField] private GameObject buyItem;      // 1

    private void Awake()
    {
        // гарантія, що менеджер існує навіть якщо ти його не ставив у сцену
        System.Type t = typeof(ShopManager); // торкнутись типу, щоб спрацював EnsureCreatedEarly
    }

    private void Start()
    {
        if (priceText != null) priceText.text = itemPrice + " $";
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(BuyItem);
        }

        // 1) миттєво відновимо
        RestoreState();
        // 2) ще раз у наступному кадрі — щоб перекрити сторонні скрипти, які міняють активність у Start
        StartCoroutine(RestoreNextFrame());
    }

    private void OnEnable()
    {
        // На випадок повторної активації об'єкта
        RestoreState();
    }

    private IEnumerator RestoreNextFrame()
    {
        yield return null; // почекати 1 кадр
        RestoreState();
    }

    private void BuyItem()
    {
        var mgr = ShopManager.Instance;
        if (mgr == null) { Debug.LogError("ShopManager не знайдено"); return; }

        if (mgr.TryBuyItem(itemID, itemPrice))
        {
            MarkAsPurchasedUI();
            SetModelActive(1); // куплена модель

            // Оновити монети, якщо маєш UI-скрипт
            FindObjectOfType<CoinsDisplay>()?.RefreshCoinsUI();

            Debug.Log($"{itemID} куплений за {itemPrice} монет.");
        }
        else
        {
            if (mgr.IsPurchased(itemID))
                Debug.Log($"{itemID} вже куплений.");
            else
                Debug.Log("Недостатньо монет.");
        }
    }

    private void RestoreState()
    {
        var mgr = ShopManager.Instance;
        if (mgr == null) return;

        bool purchased = mgr.IsPurchased(itemID);
        int modelState = mgr.GetActiveModelState(itemID); // 0=стандарт, 1=куплений

        if (purchased) MarkAsPurchasedUI();
        SetModelActive(modelState);
    }

    private void MarkAsPurchasedUI()
    {
        if (buyButton != null) buyButton.interactable = false;
        if (priceText != null) priceText.text = "Purchased";
    }

    private void SetModelActive(int state01)
    {
        bool useBought = state01 > 0;

        // Перемикаємо модельки
        if (standartItem != null) standartItem.SetActive(!useBought);
        if (buyItem != null) buyItem.SetActive(useBought);

        // Зберігаємо вибір
        ShopManager.Instance?.SetActiveModelState(itemID, useBought ? 1 : 0);
    }

    // (необов'язково) викликати з кнопки "Використати стандартний / куплений"
    public void UseStandardModel() => SetModelActive(0);
    public void UsePurchasedModel() => SetModelActive(1);
}
