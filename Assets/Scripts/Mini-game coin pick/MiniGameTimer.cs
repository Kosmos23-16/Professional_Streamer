using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameTimer : MonoBehaviour
{
    public Button targetButton;
    public Text timerText;
    public int cooldownMinutes = 15;

    private DateTime endTime;
    private bool isTimerRunning;

    void Start()
    {
        if (PlayerPrefs.HasKey("EndTime"))
        {
            endTime = DateTime.Parse(PlayerPrefs.GetString("EndTime"));
            isTimerRunning = true;
        }
        else
        {
            StartCooldown();
        }

        targetButton.onClick.AddListener(OnButtonClicked);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            TimeSpan remaining = endTime - DateTime.Now;

            if (remaining.TotalSeconds > 0)
            {
                targetButton.interactable = false;
                timerText.text = FormatTime((int)remaining.TotalSeconds);
            }
            else
            {
                isTimerRunning = false;
                targetButton.interactable = true;
                timerText.text = "";
            }
        }
    }

    private void OnButtonClicked()
    {
        StartCooldown();
    }

    private void StartCooldown()
    {
        endTime = DateTime.Now.AddMinutes(cooldownMinutes);
        PlayerPrefs.SetString("EndTime", endTime.ToString());
        PlayerPrefs.Save();
        isTimerRunning = true;
    }

    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }
}
