using UnityEngine;
using UnityEngine.UI;

public class ClickManagerForStream : MonoBehaviour
{
    [SerializeField] private int likes = 0;
    [SerializeField] private int followers = 0;
    private int coins = 0;

    [Header("Rewards")]
    [SerializeField] private int unlockAtFollowers = 100;
    [SerializeField] private int unlockAtFollowers2 = 100000;
    [SerializeField] private int unlockAtFollowers3 = 1000000;

    [Header("Rewards Prefabs")]
    [SerializeField] private GameObject rewardFigure1;
    [SerializeField] private GameObject rewardFigure2;
    [SerializeField] private GameObject rewardFigure3;

    [Header("Animation & Sound")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource normalAudioSource;
    [SerializeField] private AudioSource specialAudioSource;
    [SerializeField] private string waveTrigger = "Wave";

    [Header("Text`s")]
    public Text likesText;
    public Text followersText;
    public Text coinsText;

    private int likesToNextFollower = 0;
    private bool unlocked1 = false;
    private bool unlocked2 = false;
    private bool unlocked3 = false;

    private const string LikesKey = "likes";
    private const string FollowersKey = "followers";
    private const string CoinsKey = "coins";

    private void Start()
    {
        LoadData();
        CheckUnlocks();
    }

    public void ButtonClick()
    {
        int likeBonus = 1 + (BuffManager.Instance?.clickLikeBonus ?? 0);
        int followerLikeThreshold = 30;
        int coinBonusPerFollower = 100;

        likes += likeBonus;
        likesToNextFollower += likeBonus;

        if (likesToNextFollower >= followerLikeThreshold)
        {
            int newFollowers = likesToNextFollower / followerLikeThreshold;
            likesToNextFollower %= followerLikeThreshold;

            for (int i = 0; i < newFollowers; i++)
            {
                followers++;
                coins += coinBonusPerFollower;
                PlayCelebrateEffect(followers);
                CheckUnlocks();
            }
        }

        SaveData();
    }

    private void CheckUnlocks()
    {
        if (!unlocked1 && followers >= unlockAtFollowers)
        {
            rewardFigure1?.SetActive(true);
            unlocked1 = true;
        }

        if (!unlocked2 && followers >= unlockAtFollowers2)
        {
            rewardFigure2?.SetActive(true);
            unlocked2 = true;
        }

        if (!unlocked3 && followers >= unlockAtFollowers3)
        {
            rewardFigure3?.SetActive(true);
            unlocked3 = true;
        }
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
        coinsText.text = coins.ToString();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(LikesKey, likes);
        PlayerPrefs.SetInt(FollowersKey, followers);
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        likes = PlayerPrefs.GetInt(LikesKey, 0);
        followers = PlayerPrefs.GetInt(FollowersKey, 0);
        coins = PlayerPrefs.GetInt(CoinsKey, 0);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(LikesKey);
        PlayerPrefs.DeleteKey(FollowersKey);
        PlayerPrefs.DeleteKey(CoinsKey);
        PlayerPrefs.Save();

        likes = 0;
        followers = 0;
        coins = 0;

        unlocked1 = unlocked2 = unlocked3 = false;

        rewardFigure1?.SetActive(false);
        rewardFigure2?.SetActive(false);
        rewardFigure3?.SetActive(false);
    }
}
