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

    private int likesToNextFollower = 0;
    private bool unlocked1 = false;
    private bool unlocked2 = false;
    private bool unlocked3 = false;

    public Text likesText;
    public Text followersText;

    public void ButtonClick()
    {
        likes++;
        likesToNextFollower++;

        if (likesToNextFollower >= 10)
        {
            int newFollowers = likesToNextFollower / 10;
            followers += newFollowers;
            likesToNextFollower %= 10;

            CheckUnlocks();
        }
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

    void Update()
    {
        likesText.text = likes.ToString();
        followersText.text = followers.ToString();
    }
}