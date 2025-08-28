using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public string achievementId;
    public Text rewardMessageText;
    public string customMessage;
    private Button button;

    private string messageKey;

    void Start()
    {
        button = GetComponent<Button>();
        messageKey = $"{achievementId}_message";

        UpdateInteractable();

        if (rewardMessageText != null)
        {
            string savedMessage = PlayerPrefs.GetString(messageKey, "");
            if (!string.IsNullOrEmpty(savedMessage))
            {
                rewardMessageText.text = savedMessage;
                rewardMessageText.gameObject.SetActive(true);
            }
            else
            {
                rewardMessageText.gameObject.SetActive(false);
            }
        }
    }

    public void ClaimReward()
    {
        AchievementManager manager = FindObjectOfType<AchievementManager>();
        if (manager != null)
        {
            manager.ClaimReward(achievementId);
            FindObjectOfType<ClickManagerForStream>()?.RefreshCoins();
            FindObjectOfType<CoinsDisplay>()?.RefreshCoinsUI();

            ShowRewardMessage();
            UpdateInteractable();
        }
    }

    private void ShowRewardMessage()
    {
        if (rewardMessageText != null)
        {
            rewardMessageText.text = customMessage;
            rewardMessageText.gameObject.SetActive(true);

            PlayerPrefs.SetString(messageKey, customMessage);
            PlayerPrefs.Save();
        }
    }

    public void UpdateInteractable()
    {
        AchievementManager manager = FindObjectOfType<AchievementManager>();
        if (manager != null)
        {
            foreach (var ach in manager.achievements)
            {
                if (ach.id == achievementId)
                {
                    button.interactable = ach.isUnlocked && !ach.rewardClaimed;
                    break;
                }
            }
        }
    }
}
