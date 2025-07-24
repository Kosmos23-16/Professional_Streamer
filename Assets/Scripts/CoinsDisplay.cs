using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text followersText;
    
    private const string FollowersKey = "followers";
    private const string CoinsKey = "coins";

    void Start()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey, 0);
        coinsText.text = coins.ToString();
        
        int followers = PlayerPrefs.GetInt(FollowersKey, 0);
        followersText.text = followers.ToString();
    }
}
