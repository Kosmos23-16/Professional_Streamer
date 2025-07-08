using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public int sceneNumberToLoad = 1;

    public void SwitchScene()
    {
        if (sceneNumberToLoad >= 0 && sceneNumberToLoad < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneNumberToLoad);
        }
    }
}