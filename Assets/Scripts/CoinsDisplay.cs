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
        int coins = PlayerPrefs.GetInt(CoinsKey, 0);
        coinsText.text = coins.ToString();
        
        int followers = PlayerPrefs.GetInt(FollowersKey, 0);
        followersText.text = followers.ToString();
        
        int likes = PlayerPrefs.GetInt(LikesKey, 0);
        likesText.text = likes.ToString();
    }
}
