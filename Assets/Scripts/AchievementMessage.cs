using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementMessage : MonoBehaviour
{
    public Text messageText;
    public float showTime = 2f;

    private void OnEnable()
    {
        AchievementManager.OnAchievementUnlocked += ShowMessage;
    }

    private void OnDisable()
    {
        AchievementManager.OnAchievementUnlocked -= ShowMessage;
    }

    private void ShowMessage(Achievement achievement)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(achievement.title));
    }

    private IEnumerator ShowMessageRoutine(string title)
    {
        messageText.text = $"Achievement completed: {title}";
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(showTime);

        messageText.gameObject.SetActive(false);
    }
}
