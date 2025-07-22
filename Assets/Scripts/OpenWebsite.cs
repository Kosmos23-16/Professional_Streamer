using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsite : MonoBehaviour
{
    [Header("URL to Open")] public string url = "https://sites.google.com/d/1llfcnr7xXOPnYKWBjgXhxMuAs1XfBhCd/p/1jeI853W_qX-0amU8eRWyF3uFNjclCk3f/edit";
        
    public void OpenSite()
    {
        Application.OpenURL(url);
    }
}

