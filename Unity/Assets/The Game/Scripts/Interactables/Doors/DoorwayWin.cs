using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayWin : DoorwaySnap
{
    [SerializeField]
    string SceneName;

    public override void ExitRoom()
    {
        ChangeScene.ChangeToScene(SceneName);
    }
}