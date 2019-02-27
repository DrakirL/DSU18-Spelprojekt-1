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
        Camera.main.GetComponent<CameraMove>().OnLevelEnter += EnterRoom;
        Camera.main.GetComponent<LevelResetter>().AfterResetLevel += StartRoom;
    }

    public virtual void ExitRoom()
    {
        if (Exit == null)
            return;

        CameraMove cameraMove = Camera.main.GetComponent<CameraMove>();
        cameraMove.EnterLevel(Exit.Room);
    }

    void EnterRoom(Transform newRoom)
    {
        if (Room != newRoom)
            return;
        
        StartRoom(newRoom);
        PlayerPrefs.SetString("SavedRoom", Room.name);
    }

    protected virtual void StartRoom(Transform newRoom)
    {
        if (Room != newRoom)
            return;

        GameObject.Find("Player").transform.position = transform.position;
    }
}