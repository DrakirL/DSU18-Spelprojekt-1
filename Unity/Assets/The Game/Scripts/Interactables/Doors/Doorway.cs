using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    DOWN,
    UP,
    LEFT,
    RIGHT
}

public abstract class Doorway : MonoBehaviour
{
    public Doorway Exit;

    public Transform Room => transform.parent.parent;

    public Vector2 directionFromEnum(Direction dir)
    {
        if (dir == Direction.DOWN)
            return Vector2.down;

        else if (dir == Direction.UP)
            return Vector2.up;

        else if (dir == Direction.LEFT)
            return Vector2.left;

        else
            return Vector2.right;
    }

    protected void Awake()
    {
        var cam = Camera.main;

        DoorwayTransitions.OnEnteredDoor += EnterRoom;
        cam.GetComponent<LevelResetter>().AfterResetLevel += StartRoom;
    }

    public virtual void ExitRoom()
    {
        if (Exit == null)
            return;

        GameObject.Find("Player").transform.position = transform.position;
        DoorwayTransitions.Enter(this);
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