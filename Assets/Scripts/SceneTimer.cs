using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    [SerializeField] private string sceneToLoad = "Main";
    [SerializeField] private Text timerText;

    private float timer;
    private bool timerActive = false;

    private float blinkInterval = 0.5f;
    private float blinkTimer = 0f;
    private bool isTextVisible = true;

    void Start()
    {
        timer = timeLimit;
        timerActive = true;
    }

    void Update()
    {
        if (!timerActive) return;

        timer -= Time.deltaTime;

        if (timer <= 10f)
        {
            timerText.color = Color.red;

            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                isTextVisible = !isTextVisible;
                timerText.enabled = isTextVisible;
                blinkTimer = 0f;
            }
        }
        else
        {
            timerText.color = Color.white;
            timerText.enabled = true;
        }

        timerText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0f)
        {
            timerActive = false;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}