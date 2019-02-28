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

public class Doorway : MonoBehaviour
{
    public Direction Orientation;
    public Doorway Exit;

    public Transform Room => transform.parent.parent;
    CameraMove camMove;

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
        camMove = cam.GetComponent<CameraMove>();
        camMove.OnLevelEnter += EnterRoom;
        cam.GetComponent<LevelResetter>().AfterResetLevel += StartRoom;
    }

    public virtual void ExitRoom()
    {
        if (Exit == null)
            return;

        CameraMove cameraMove = Camera.main.GetComponent<CameraMove>();
        cameraMove.EnterLevel(Exit);
    }

    void EnterRoom(Doorway door)
    {
        if (this != door)
            return;
        
        StartRoom(door);
        PlayerPrefs.SetString("SavedRoom", door.transform.name);
    }

    protected virtual void StartRoom(Doorway door)
    {
        if (this != door)
            return;

        GameObject.Find("Player").transform.position = transform.position;
    }
}