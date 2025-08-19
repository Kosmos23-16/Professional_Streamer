using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public string achievementId;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        UpdateInteractable();
    }

    public void ClaimReward()
    {
        AchievementManager manager = FindObjectOfType<AchievementManager>();
        if (manager != null)
        {
            manager.ClaimReward(achievementId);
            FindObjectOfType<ClickManagerForStream>()?.RefreshCoins();
            FindObjectOfType<CoinsDisplay>()?.RefreshCoinsUI();

            UpdateInteractable();
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
