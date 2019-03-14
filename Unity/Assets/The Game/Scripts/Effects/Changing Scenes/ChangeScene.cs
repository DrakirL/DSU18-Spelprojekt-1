using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static event System.Action OnSceneChanged;

    public string sceneName;

    public void Change() => ChangeScene.ChangeToScene(sceneName);
    public void ChangeTo(string to) => ChangeScene.ChangeToScene(to);

    public static void ChangeToScene(string to)
    {
        OnSceneChanged?.Invoke();
        OnSceneChanged = null;
        SceneManager.LoadScene(to);
    }
}