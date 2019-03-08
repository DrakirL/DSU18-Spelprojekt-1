using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoorwayTransitions
{
    public static Transform CurrentRoom => CurrentDoor?.transform.parent.parent ?? null;
    public static Transform NextRoom => NextDoor?.transform.parent.parent ?? null;

    public static Doorway NextDoor;
    public static Doorway CurrentDoor;

    public static event System.Action BeforeEnteredDoor;
    public static event System.Action OnEnteredDoor;
    public static event System.Action AfterExitedDoor;
    public static event System.Action Done;

    public static void Start(Doorway door)
    {
        CurrentDoor = door;
        NextDoor = door;
        OnEnteredDoor?.Invoke();

        Save();
    }

    public static void Enter(Doorway door)
    {
        if (door == null)
            Debug.LogError("Door is null");

        CurrentDoor = door;
        NextDoor = door.Exit;

        OmniDisabler.Disable();
        BeforeEnteredDoor?.Invoke();

        Save();
    }

    public static void FinishMoveToRoom()
    {
        CurrentDoor = NextDoor;
        NextDoor = null;
        AfterExitedDoor?.Invoke();
    }


    static void Save()
    {
        var roomName = DoorwayTransitions.NextRoom.name;
        var doorName = DoorwayTransitions.NextDoor.name;

        PlayerPrefs.SetString("SavedRoom", roomName);
        PlayerPrefs.SetString("SavedDoor", doorName);
    }

    public static void FinishBeforeEnter()
    {
        OnEnteredDoor?.Invoke();
    }
    public static void FinishOnEnter()
    {
        Debug.Log("finished on enter");
        AfterExitedDoor?.Invoke();
    }
    public static void FinishAfterEnter()
    {
        OmniDisabler.Enable();
        Done?.Invoke();
    }
}