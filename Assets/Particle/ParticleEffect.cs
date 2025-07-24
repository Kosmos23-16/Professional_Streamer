using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleEffect : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float duration = 1f;
    private RectTransform rectTransform;
    private float timeElapsed;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        rectTransform.anchoredPosition += new Vector2(0, moveSpeed) * Time.deltaTime;

        if (timeElapsed > duration)
            Destroy(gameObject);
    }
}
