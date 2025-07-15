using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string url = "https://sites.google.com/view/professionalstreamer/professional-streamer?authuser=0";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
