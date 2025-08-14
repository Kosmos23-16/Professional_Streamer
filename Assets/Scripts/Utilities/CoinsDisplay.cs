using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text followersText;
    [SerializeField] private Text likesText;

    private const string FollowersKey = "followers";
    private const string CoinsKey = "coins";
    private const string LikesKey = "likes";

    void Start()
    {
        RefreshCoinsUI();
    }

    public void RefreshCoinsUI()
    {
        coinsText.text = PlayerPrefs.GetInt(CoinsKey, 0).ToString();
        followersText.text = PlayerPrefs.GetInt(FollowersKey, 0).ToString();
        likesText.text = PlayerPrefs.GetInt(LikesKey, 0).ToString();
    }
}
