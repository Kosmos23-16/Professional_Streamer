using UnityEngine;

using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumeSlider.value = audioSource.volume;
    }
    public void ChangeVolume()
    {   
        audioSource.volume = volumeSlider.value;
    }
}