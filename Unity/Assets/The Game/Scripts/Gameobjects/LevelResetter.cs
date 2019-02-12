using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelResetter : MonoBehaviour
{
    public event System.Action<float> BeforeLevelReset;
    public event System.Action<Transform> OnLevelReset;

    CameraMove cameraMove;
    public float duration;

    private void Start()
    {
        cameraMove = GetComponent<CameraMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetLevel();
    }

    public void ResetLevel()
    {
        BeforeLevelReset?.Invoke(duration);

        OnLevelReset?.Invoke(cameraMove.currentRoom);

        //Stop covering screen
    }
}