using UnityEngine;
using UnityEngine.UI;

public class ClickManagerForStream : MonoBehaviour
{
    [SerializeField] private int likes = 0;
    [SerializeField] private int followers = 0;

    [Header("Rewards")]
    [SerializeField] private int unlockAtFollowers = 100;
    [SerializeField] private int unlockAtFollowers2 = 100000;
    [SerializeField] private int unlockAtFollowers3 = 1000000;

    [Header("Rewards Prefabs")]
    [SerializeField] private GameObject rewardFigure1;
    [SerializeField] private GameObject rewardFigure2;
    [SerializeField] private GameObject rewardFigure3;

    [Header("Reward UI Images")]
    [SerializeField] private Image rewardImage1;
    [SerializeField] private Image rewardImage2;
    [SerializeField] private Image rewardImage3;

    [Header("Animation & Sound")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource normalAudioSource;
    [SerializeField] private AudioSource specialAudioSource;
    [SerializeField] private string waveTrigger = "Wave";

    [Header("UI Texts")]
    public Text likesText;
    public Text followersText;
    public Text coinsText;

    private int likeBonus = 1;
    private int followerLikeThreshold = 50;
    private int coinBonusPerFollower = 100;

    private float likeMultiplier = 1f;
    private float coinMultiplier = 1f;

    private int likesToNextFollower = 0;
    private bool unlocked1 = false;
    private bool unlocked2 = false;
    private bool unlocked3 = false;

    private const string LikesKey = "likes";
    private const string FollowersKey = "followers";
    private const string CoinsKey = "coins";
    private const string Unlock1Key = "unlock1";
    private const string Unlock2Key = "unlock2";
    private const string Unlock3Key = "unlock3";

    private void Start()
    {
        // Спочатку всі картинки вимикаємо (на випадок якщо забудеш в інспекторі)
        rewardImage1?.gameObject.SetActive(false);
        rewardImage2?.gameObject.SetActive(false);
        rewardImage3?.gameObject.SetActive(false);

        LoadData();
        LoadBuffs();
        CheckUnlocks();
        RefreshCoins();
    }

    public void ButtonClick()
    {
        int gainedLikes = Mathf.RoundToInt(likeBonus * likeMultiplier);
        likes += gainedLikes;
        likesToNextFollower += gainedLikes;

        FindObjectOfType<AchievementManager>()?.AddProgress("click_100", gainedLikes);

        if (likesToNextFollower >= followerLikeThreshold)
        {
            int newFollowers = likesToNextFollower / followerLikeThreshold;
            likesToNextFollower %= followerLikeThreshold;

            for (int i = 0; i < newFollowers; i++)
            {
                followers++;

                int coins = PlayerPrefs.GetInt(CoinsKey, 0);
                int gainedCoins = Mathf.RoundToInt(coinBonusPerFollower * coinMultiplier);
                coins += gainedCoins;
                PlayerPrefs.SetInt(CoinsKey, coins);
                PlayerPrefs.Save();

                PlayCelebrateEffect(followers);
                CheckUnlocks();
                RefreshCoins();
            }
        }

        SaveData();
    }

    private void CheckUnlocks()
    {
        if (!unlocked1 && followers >= unlockAtFollowers)
        {
            rewardFigure1?.SetActive(true);
            rewardImage1?.gameObject.SetActive(true);
            unlocked1 = true;
            PlayerPrefs.SetInt(Unlock1Key, 1);
        }

        if (!unlocked2 && followers >= unlockAtFollowers2)
        {
            rewardFigure2?.SetActive(true);
            rewardImage2?.gameObject.SetActive(true);
            unlocked2 = true;
            PlayerPrefs.SetInt(Unlock2Key, 1);
        }

        if (!unlocked3 && followers >= unlockAtFollowers3)
        {
            rewardFigure3?.SetActive(true);
            rewardImage3?.gameObject.SetActive(true);
            unlocked3 = true;
            PlayerPrefs.SetInt(Unlock3Key, 1);
        }

        PlayerPrefs.Save();
    }

    private void PlayCelebrateEffect(int currentFollower)
    {
        if (animator != null)
            animator.SetTrigger(waveTrigger);

        if (currentFollower % 2 == 0)
        {
            if (specialAudioSource != null && !specialAudioSource.isPlaying)
                specialAudioSource.Play();
        }
        else
        {
            if (normalAudioSource != null && !normalAudioSource.isPlaying)
                normalAudioSource.Play();
        }
    }

    void Update()
    {
        likesText.text = likes.ToString();
        followersText.text = followers.ToString();
        coinsText.text = PlayerPrefs.GetInt(CoinsKey, 0).ToString();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(LikesKey, likes);
        PlayerPrefs.SetInt(FollowersKey, followers);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        likes = PlayerPrefs.GetInt(LikesKey, 0);
        followers = PlayerPrefs.GetInt(FollowersKey, 0);

        unlocked1 = PlayerPrefs.GetInt(Unlock1Key, 0) == 1;
        unlocked2 = PlayerPrefs.GetInt(Unlock2Key, 0) == 1;
        unlocked3 = PlayerPrefs.GetInt(Unlock3Key, 0) == 1;

        if (unlocked1)
        {
            rewardFigure1?.SetActive(true);
            rewardImage1?.gameObject.SetActive(true);
        }
        if (unlocked2)
        {
            rewardFigure2?.SetActive(true);
            rewardImage2?.gameObject.SetActive(true);
        }
        if (unlocked3)
        {
            rewardFigure3?.SetActive(true);
            rewardImage3?.gameObject.SetActive(true);
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(LikesKey);
        PlayerPrefs.DeleteKey(FollowersKey);
        PlayerPrefs.DeleteKey(CoinsKey);
        PlayerPrefs.DeleteKey(Unlock1Key);
        PlayerPrefs.DeleteKey(Unlock2Key);
        PlayerPrefs.DeleteKey(Unlock3Key);
        PlayerPrefs.Save();

        likes = 0;
        followers = 0;

        unlocked1 = unlocked2 = unlocked3 = false;

        rewardFigure1?.SetActive(false);
        rewardFigure2?.SetActive(false);
        rewardFigure3?.SetActive(false);

        rewardImage1?.gameObject.SetActive(false);
        rewardImage2?.gameObject.SetActive(false);
        rewardImage3?.gameObject.SetActive(false);

        RefreshCoins();
    }

    public void StopGame()
    {
        SaveData();
        Debug.Log("Дані збережено перед зупинкою!");
    }

    public void RefreshCoins()
    {
        if (coinsText != null)
        {
            coinsText.text = PlayerPrefs.GetInt(CoinsKey, 0).ToString();
        }
    }

    private void LoadBuffs()
    {
        likeMultiplier = 1f;

        likeMultiplier += PlayerPrefs.GetInt("shop_item_mouse_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_keyboard_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_monitor_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_camera_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_micro_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_headphones_level", 0) * 1f;
        likeMultiplier += PlayerPrefs.GetInt("shop_item_chair_level", 0) * 1f;

        Debug.Log($"Баффи завантажені: likeMultiplier={likeMultiplier}, coinMultiplier={coinMultiplier}");
    }
}
