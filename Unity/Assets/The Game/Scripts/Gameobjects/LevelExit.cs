using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public Transform TargetRoom;

   public void ExitRoom()
    {
        CameraMove cameraMove = Camera.main.GetComponent<CameraMove>();

        if (cameraMove.currentRoom != TargetRoom)
        {
            //Disable the level
            cameraMove.EnterLevel(TargetRoom);
        }
    }
}