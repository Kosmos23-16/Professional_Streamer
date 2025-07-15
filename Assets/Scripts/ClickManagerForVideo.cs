using UnityEngine;
using UnityEngine.UI;

public class ClickManagerForVideo : MonoBehaviour
{
    [SerializeField] private int likes = 0;
    [SerializeField] private int followers = 0;

    [Header("Rewards")]
    [SerializeField] private int unlockAtFollowers = 100;
    [SerializeField] private int unlockAtFollowers2 = 100000;
    [SerializeField] private int unlockAtFollowers3 = 1000000;

    private int likesToNextFollower = 0;

    public Text likesText;
    public Text followersText;

    public void ButtonClick()
    {
        likes++;
        likesToNextFollower++;

        if (likesToNextFollower >= 30)
        {
            int newFollowers = likesToNextFollower / 30;
            followers += newFollowers;
            likesToNextFollower %= 30;
        }
    }

    void Update()
    {
        likesText.text = likes.ToString();
        followersText.text = followers.ToString();
    }
}