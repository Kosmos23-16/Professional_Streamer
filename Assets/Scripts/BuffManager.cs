using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;

    public int clickLikeBonus = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBuffs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyBuff(string itemID)
    {
        switch (itemID)
        {
            case "buff_click_1":
                clickLikeBonus += 1;
                break;
            case "buff_click_2":
                clickLikeBonus += 2;
                break;
            case "buff_click_3":
                clickLikeBonus += 3;
                break;
            case "buff_click_4":
                clickLikeBonus += 4;
                break;
            case "buff_click_5":
                clickLikeBonus += 5;
                break;
        }
    }

    public void LoadBuffs()
    {
        foreach (var id in ShopItemIDs.All)
        {
            if (PlayerPrefs.GetInt($"shop_item_{id}_purchased", 0) == 1)
            {
                ApplyBuff(id);
            }
        }
    }
}
