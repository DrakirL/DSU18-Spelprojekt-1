using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public void Change()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeTo(string to)
    {
        SceneManager.LoadScene(to);
    }
    public static void ChangeToScene(string to)
    {
        SceneManager.LoadScene(to);
    }
}