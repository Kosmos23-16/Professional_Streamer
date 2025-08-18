using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Achievement
{
    public string id;
    public string name;
    public int requiredProgress;
    public int rewardCoins;
    public int currentProgress;
    public bool isUnlocked;
    public bool rewardClaimed;
}

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements = new List<Achievement>();

    private void Start()
    {
        LoadAchievements();
    }

    public void AddProgress(string id, int amount)
    {
        Achievement ach = GetAchievementById(id);
        if (ach != null && !ach.isUnlocked)
        {
            ach.currentProgress += amount;
            if (ach.currentProgress >= ach.requiredProgress)
            {
                ach.isUnlocked = true;
                ach.currentProgress = ach.requiredProgress;
                Debug.Log($"Ачівка '{ach.name}' виконана!");
            }
            SaveAchievements();
        }
    }

    public Achievement GetAchievementById(string id)
    {
        return achievements.Find(a => a.id == id);
    }

    public void SaveAchievements()
    {
        foreach (var ach in achievements)
        {
            PlayerPrefs.SetInt(ach.id + "_progress", ach.currentProgress);
            PlayerPrefs.SetInt(ach.id + "_unlocked", ach.isUnlocked ? 1 : 0);
            PlayerPrefs.SetInt(ach.id + "_claimed", ach.rewardClaimed ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadAchievements()
    {
        foreach (var ach in achievements)
        {
            ach.currentProgress = PlayerPrefs.GetInt(ach.id + "_progress", 0);
            ach.isUnlocked = PlayerPrefs.GetInt(ach.id + "_unlocked", 0) == 1;
            ach.rewardClaimed = PlayerPrefs.GetInt(ach.id + "_claimed", 0) == 1;
        }
    }
}
