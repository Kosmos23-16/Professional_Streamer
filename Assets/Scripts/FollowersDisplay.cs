using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowersDisplay : MonoBehaviour
{
    [SerializeField] private Text followersText;

    void Start()
    {
        int followers = PlayerPrefs.GetInt("followers", 0);
        followersText.text = followers.ToString();
    }
}
