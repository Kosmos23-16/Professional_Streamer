using UnityEngine;

public class AchievementButton : MonoBehaviour
{
    public string achievementId;

    public void ClaimReward()
    {
        AchievementManager manager = FindObjectOfType<AchievementManager>();
        if (manager != null)
        {
            manager.ClaimReward(achievementId);
        }
    }
}
