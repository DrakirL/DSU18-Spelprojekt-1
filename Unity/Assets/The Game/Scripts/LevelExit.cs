using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    MenuMaster menuMaster;

    public static event System.Action OnExitStart;
    public static event System.Action OnExitEnd;

    [SerializeField]
    string targetScene;

    [SerializeField]
    float exitDelay;

    private void Start()
    {
        menuMaster = GetComponent<MenuMaster>();
    }

    public void Exit()
    {
        OnExitStart?.Invoke();
        Invoke("NextLevel", exitDelay);

    }

    void NextLevel()
    {
        menuMaster.ChangeScene(targetScene);
        OnExitEnd?.Invoke();

    }
}