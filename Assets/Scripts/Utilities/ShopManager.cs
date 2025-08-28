using UnityEngine;

[DefaultExecutionOrder(-200)] // менеджер ініціалізується дуже рано
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    private const string CoinsKey = "coins";

    // ── Автостворення менеджера, якщо його нема у сцені ──────────────────────────
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureCreatedEarly()
    {
        if (Instance != null) return;
        var go = new GameObject("ShopManager(Auto)");
        go.AddComponent<ShopManager>();
        Object.DontDestroyOnLoad(go);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ── Ключі у PlayerPrefs ──────────────────────────────────────────────────────
    public static string PurchaseKey(string itemID) => $"shop_item_{itemID}_purchased";
    public static string ActiveModelKey(string itemID) => $"shop_item_{itemID}_activeModel"; // 0=стандарт, 1=куплений

    // ── Монети ───────────────────────────────────────────────────────────────────
    public int GetCoins() => PlayerPrefs.GetInt(CoinsKey, 0);

    public void SetCoins(int value)
    {
        PlayerPrefs.SetInt(CoinsKey, Mathf.Max(0, value));
        PlayerPrefs.Save();
    }

    public bool TrySpendCoins(int price, out int newBalance)
    {
        int coins = GetCoins();
        if (coins < price) { newBalance = coins; return false; }
        coins -= price;
        SetCoins(coins);
        newBalance = coins;
        return true;
    }

    // ── Покупки ──────────────────────────────────────────────────────────────────
    public bool IsPurchased(string itemID)
        => PlayerPrefs.GetInt(PurchaseKey(itemID), 0) == 1;

    public void MarkPurchased(string itemID)
    {
        PlayerPrefs.SetInt(PurchaseKey(itemID), 1);
        // За замовчуванням увімкнемо куплену модель після покупки
        PlayerPrefs.SetInt(ActiveModelKey(itemID), 1);
        PlayerPrefs.Save();
    }

    public bool TryBuyItem(string itemID, int price)
    {
        if (IsPurchased(itemID)) return false;
        if (!TrySpendCoins(price, out _)) return false;

        MarkPurchased(itemID);
        return true;
    }

    // ── Активна моделька (скин) ─────────────────────────────────────────────────
    // Повертає 0 або 1. Якщо предмет куплений, дефолт — 1 (куплений).
    public int GetActiveModelState(string itemID)
    {
        int def = IsPurchased(itemID) ? 1 : 0;
        return PlayerPrefs.GetInt(ActiveModelKey(itemID), def);
    }

    public void SetActiveModelState(string itemID, int state01)
    {
        PlayerPrefs.SetInt(ActiveModelKey(itemID), state01 > 0 ? 1 : 0);
        PlayerPrefs.Save();
    }
}
