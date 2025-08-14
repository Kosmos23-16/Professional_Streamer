using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string id;
        public string title;
        public int targetValue; 
        public int rewardCoins;
        public bool isUnlocked;
        public bool rewardClaimed;
    }

    public Achievement[] achievements;
    public Text[] achievementTexts;

    private const string CoinsKey = "coins";

    void Start()
    {
        LoadAchievements();
        UpdateUI();
    }

    public void AddProgress(string achievementId, int amount)
    {
        foreach (var ach in achievements)
        {
            if (ach.id == achievementId && !ach.isUnlocked)
            {
                int current = PlayerPrefs.GetInt(achievementId + "_progress", 0);
                current += amount;
                PlayerPrefs.SetInt(achievementId + "_progress", current);

                if (current >= ach.targetValue)
                {
                    ach.isUnlocked = true;
                    PlayerPrefs.SetInt(achievementId + "_unlocked", 1);
                }
            }
        }
        PlayerPrefs.Save();
        UpdateUI();
    }
    public void ClaimReward(string achievementId)
    {
        foreach (var ach in achievements)
        {
            if (ach.id == achievementId && ach.isUnlocked && !ach.rewardClaimed)
            {
                int coins = PlayerPrefs.GetInt(CoinsKey, 0);
                coins += ach.rewardCoins;
                PlayerPrefs.SetInt(CoinsKey, coins);

                ach.rewardClaimed = true;
                PlayerPrefs.SetInt(achievementId + "_claimed", 1);

                PlayerPrefs.Save();
                UpdateUI();
                Debug.Log("Нагорода отримана: " + ach.rewardCoins + " монет!");
            }
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            var ach = achievements[i];
            int progress = PlayerPrefs.GetInt(ach.id + "_progress", 0);
            string status = ach.isUnlocked ? (ach.rewardClaimed ? "✅ Отримано" : "🎁 Доступна нагорода") : progress + "/" + ach.targetValue;
            achievementTexts[i].text = ach.title + " — " + status;
        }
    }

    private void LoadAchievements()
    {
        foreach (var ach in achievements)
        {
            ach.isUnlocked = PlayerPrefs.GetInt(ach.id + "_unlocked", 0) == 1;
            ach.rewardClaimed = PlayerPrefs.GetInt(ach.id + "_claimed", 0) == 1;
        }
    }

    public void ResetAchievements()
    {
        foreach (var ach in achievements)
        {
            PlayerPrefs.DeleteKey(ach.id + "_progress");
            PlayerPrefs.DeleteKey(ach.id + "_unlocked");
            PlayerPrefs.DeleteKey(ach.id + "_claimed");
        }
        PlayerPrefs.Save();
        LoadAchievements();
        UpdateUI();
    }
}
