using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Achievement
{
    public string id;
    public string title;
    public int targetValue;
    public int rewardCoins;

    [Header("Optional Reward Object")]
    public GameObject rewardObject; // 🔹 Об'єкт, який стане активним після отримання нагороди

    public bool isUnlocked;
    public bool rewardClaimed;
    public int currentValue;
}

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements = new List<Achievement>();
    private const string CoinsKey = "coins";

    public static event Action<Achievement> OnAchievementUnlocked;

    void Awake()
    {
        LoadAchievements();
    }

    public void AddProgress(string achievementId, int amount)
    {
        foreach (var ach in achievements)
        {
            if (ach.id == achievementId && !ach.isUnlocked)
            {
                ach.currentValue += amount;
                if (ach.currentValue >= ach.targetValue)
                {
                    ach.isUnlocked = true;
                    Debug.Log($"Achievement unlocked: {ach.title}");

                    OnAchievementUnlocked?.Invoke(ach);
                }
            }
        }
        SaveAchievements();
    }

    public void ClaimReward(string achievementId)
    {
        foreach (var ach in achievements)
        {
            if (ach.id == achievementId && ach.isUnlocked && !ach.rewardClaimed)
            {
                ach.rewardClaimed = true;

                // 🔹 Додаємо монети
                int coins = PlayerPrefs.GetInt(CoinsKey, 0);
                coins += ach.rewardCoins;
                PlayerPrefs.SetInt(CoinsKey, coins);

                // 🔹 Активуємо об’єкт
                if (ach.rewardObject != null)
                {
                    ach.rewardObject.SetActive(true);
                    PlayerPrefs.SetInt($"{ach.rewardObject.name}_active", 1);
                    Debug.Log($"Reward object activated: {ach.rewardObject.name}");
                }

                PlayerPrefs.Save();
                Debug.Log($"Reward claimed: +{ach.rewardCoins} coins");
            }
        }
        SaveAchievements();
    }

    private void SaveAchievements()
    {
        foreach (var ach in achievements)
        {
            PlayerPrefs.SetInt($"{ach.id}_current", ach.currentValue);
            PlayerPrefs.SetInt($"{ach.id}_unlocked", ach.isUnlocked ? 1 : 0);
            PlayerPrefs.SetInt($"{ach.id}_claimed", ach.rewardClaimed ? 1 : 0);

            if (ach.rewardObject != null)
            {
                int isActive = ach.rewardObject.activeSelf ? 1 : 0;
                PlayerPrefs.SetInt($"{ach.rewardObject.name}_active", isActive);
            }
        }
        PlayerPrefs.Save();
    }

    private void LoadAchievements()
    {
        foreach (var ach in achievements)
        {
            ach.currentValue = PlayerPrefs.GetInt($"{ach.id}_current", 0);
            ach.isUnlocked = PlayerPrefs.GetInt($"{ach.id}_unlocked", 0) == 1;
            ach.rewardClaimed = PlayerPrefs.GetInt($"{ach.id}_claimed", 0) == 1;

            if (ach.rewardObject != null)
            {
                bool isActive = PlayerPrefs.GetInt($"{ach.rewardObject.name}_active", 0) == 1;
                ach.rewardObject.SetActive(isActive);
            }
        }
    }
}
