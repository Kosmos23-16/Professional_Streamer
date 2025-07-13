using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    private const string CoinsKey = "coins";

    void Start()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey, 0);
        coinsText.text = coins.ToString();
    }
}
