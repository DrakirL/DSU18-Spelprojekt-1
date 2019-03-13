using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelResetter : MonoBehaviour
{
    public event System.Action BeforeLevelReset;
    public event System.Action<Doorway> AfterResetLevel;

    public float duration;
    
    public void StartResetLevel() => BeforeLevelReset?.Invoke();

    public void FinishResetLevel() => AfterResetLevel?.Invoke(DoorwayTransitions.CurrentDoor);   
}