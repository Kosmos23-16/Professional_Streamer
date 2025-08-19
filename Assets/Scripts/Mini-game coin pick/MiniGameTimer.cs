using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameTimer : MonoBehaviour
{
    public Button targetButton;   // Твоя кнопка
    public Text timerText;        // Текст на кнопці (де показується таймер)
    public int cooldownMinutes = 15;

    private DateTime endTime;
    private bool isTimerRunning;

    void Start()
    {
        // Завантажуємо час завершення, якщо він збережений
        if (PlayerPrefs.HasKey("EndTime"))
        {
            endTime = DateTime.Parse(PlayerPrefs.GetString("EndTime"));
            isTimerRunning = true;
        }
        else
        {
            // Якщо перший запуск → запускаємо таймер
            StartCooldown();
        }

        // Додаємо слухача на кнопку (коли гравець натисне)
        targetButton.onClick.AddListener(OnButtonClicked);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            TimeSpan remaining = endTime - DateTime.Now;

            if (remaining.TotalSeconds > 0)
            {
                targetButton.interactable = false; // Заблокована
                timerText.text = FormatTime((int)remaining.TotalSeconds);
            }
            else
            {
                isTimerRunning = false;
                targetButton.interactable = true; // Розблокувати
                timerText.text = "Натисни!";
            }
        }
    }

    // Викликається при кліку по кнопці
    private void OnButtonClicked()
    {
        // Запускаємо новий таймер
        StartCooldown();
    }

    // Функція запуску таймера
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
