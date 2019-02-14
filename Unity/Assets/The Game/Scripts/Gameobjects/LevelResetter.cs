using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelResetter : MonoBehaviour
{
    public event System.Action BeforeLevelReset;
    public event System.Action<Transform> AfterResetLevel;

    CameraMove cameraMove;
    public float duration;

    private void Start()
    {
        cameraMove = GetComponent<CameraMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartResetLevel();
    }

    public void StartResetLevel()
    {
        BeforeLevelReset?.Invoke();
    }

    public void FinishResetLevel()
    {
        AfterResetLevel?.Invoke(cameraMove.currentRoom);
    }
}