using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelEntrance : MonoBehaviour
{
    public Direction StartingDirection;

    protected void Awake()
    {
        Camera.main.GetComponent<CameraMove>().OnLevelEnter += EnterLevel;
        Camera.main.GetComponent<LevelResetter>().AfterResetLevel += StartLevel;
    }

    public void EnterLevel(Transform newRoom)
    {
        if (transform.parent != newRoom)
            return;

        //Enable the level
        StartLevel(newRoom);
        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }

    public virtual void StartLevel(Transform room)
    {
        if (transform.parent != room)
            return;

        GameObject.Find("Player").transform.position = transform.position;
    }

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
}