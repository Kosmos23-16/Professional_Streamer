using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public string achievementId;
    public Image rewardImage;
    private Button button;

    private string imageKey;

    void Start()
    {
        button = GetComponent<Button>();
        imageKey = $"{achievementId}_image";

        UpdateInteractable();

        if (rewardImage != null)
        {
            bool isShown = PlayerPrefs.GetInt(imageKey, 0) == 1;
            rewardImage.gameObject.SetActive(isShown);
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

            ShowRewardImage();
            UpdateInteractable();
        }
    }

    private void ShowRewardImage()
    {
        if (rewardImage != null)
        {
            rewardImage.gameObject.SetActive(true);

            PlayerPrefs.SetInt(imageKey, 1);
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
