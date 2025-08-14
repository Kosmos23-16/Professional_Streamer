using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public AudioSource audio;

    public void ButtonClick()
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
