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
                int coins = PlayerPrefs.GetInt(CoinsKey, 0);
                coins += ach.rewardCoins;
                PlayerPrefs.SetInt(CoinsKey, coins);
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
        }
    }
}
