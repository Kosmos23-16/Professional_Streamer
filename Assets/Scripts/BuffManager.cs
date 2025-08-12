using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;

    public int clickLikeBonus = 0;
    public int coinBonusPerFollower = 0; 
    public int followerThresholdReduction = 0;

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
            case "buff_coin_1":
                coinBonusPerFollower += 50;
                break;
            case "buff_follower_easy":
                followerThresholdReduction += 5;
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
