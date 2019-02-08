using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelResetter : MonoBehaviour
{
    public event System.Action<Transform> OnLevelReset;

    CameraMove cameraMove;

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
        //Play some effect (that covers the screen)

        OnLevelReset?.Invoke(cameraMove.currentRoom);

        //Stop covering screen
    }
}