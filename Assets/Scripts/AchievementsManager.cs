using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image icon;
    public Text titleText;
    public Text descriptionText;
    public GameObject lockedOverlay;

    [Header("Achievement Data")]
    public string achievementKey;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public string unlockedTitle;
    public string lockedTitle;
    public string unlockedDescription;
    public string lockedDescription;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        bool unlocked = AchievementsManager.Instance != null &&
                        AchievementsManager.Instance.IsAchievementUnlocked(achievementKey);

        if (unlocked)
        {
            icon.sprite = unlockedSprite;
            titleText.text = unlockedTitle;
            descriptionText.text = unlockedDescription;
            lockedOverlay.SetActive(false);
        }
        else
        {
            icon.sprite = lockedSprite;
            titleText.text = lockedTitle;
            descriptionText.text = lockedDescription;
            lockedOverlay.SetActive(true);
        }
    }
}
