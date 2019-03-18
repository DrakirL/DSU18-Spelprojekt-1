using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : ChangeScene
{
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        
        OnSceneChanged += () => PlayerPrefs.DeleteKey("SavedRoom");
    }
}