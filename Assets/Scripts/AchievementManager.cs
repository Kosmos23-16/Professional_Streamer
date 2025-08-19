using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private Achievement[] achievements;

    [Header("UI Notification")]
    [SerializeField] private GameObject achievementNotificationPanel;
    [SerializeField] private Text achievementNotificationText;
    [SerializeField] private float notificationDuration = 3f;

    public void AddProgress(string id, int amount)
    {
        Achievement achievement = GetAchievementById(id);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.currentValue += amount;
            if (achievement.currentValue >= achievement.targetValue)
            {
                UnlockAchievement(achievement);
            }
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        achievement.isUnlocked = true;
        Debug.Log($"Ачівка виконана: {achievement.title}");

        ShowNotification($"Виконано: {achievement.title}!");
    }

    public Achievement GetAchievementById(string id)
    {
        foreach (Achievement a in achievements)
        {
            if (a.id == id)
                return a;
        }
        return null;
    }

    public void SaveAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            PlayerPrefs.SetInt($"ach_{achievement.id}_unlocked", achievement.isUnlocked ? 1 : 0);
            PlayerPrefs.SetInt($"ach_{achievement.id}_claimed", achievement.rewardClaimed ? 1 : 0);
            PlayerPrefs.SetInt($"ach_{achievement.id}_value", achievement.currentValue);
        }
        PlayerPrefs.Save();
    }

    private void ShowNotification(string message)
    {
        if (achievementNotificationPanel != null && achievementNotificationText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowNotificationRoutine(message));
        }
    }

    private IEnumerator ShowNotificationRoutine(string message)
    {
        achievementNotificationPanel.SetActive(true);
        achievementNotificationText.text = message;

        yield return new WaitForSeconds(notificationDuration);

        achievementNotificationPanel.SetActive(false);
    }
}
