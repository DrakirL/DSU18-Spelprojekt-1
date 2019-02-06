﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public Transform TargetRoom;
    public CameraMove cameraMove;

   public void ExitRoom()
    {
        if (cameraMove.currentRoom != TargetRoom)
        {
            cameraMove.EnterLevel(TargetRoom);
        }
    }
}