using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Doorway : MonoBehaviour
{
    public Doorway Exit;

    public Transform Room => transform.parent.parent;

    protected void Awake()
    {
        var cam = Camera.main;

        DoorwayTransitions.OnEnteredDoor += EnterRoom;
        cam.GetComponent<LevelResetter>().AfterResetLevel += StartRoom;
    }

    public virtual void ExitRoom()
    {
        Debug.Log("ExitDoor() Start");

        if (Exit == null)
            return;

        GameObject.Find("Player").transform.position = transform.position;
        DoorwayTransitions.Enter(this);

        Debug.Log("ExitDoor() End");
    }

    void EnterRoom()
    {
        var nextDoor = DoorwayTransitions.NextDoor;
        if (this != nextDoor)
            return;

        StartRoom(this);
    }

    protected virtual void StartRoom(Doorway door)
    {
        if (this != door)
            return;

        GameObject.Find("Player").transform.position = transform.position;
    }
}