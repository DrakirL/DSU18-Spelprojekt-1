﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntranceSnap : MonoBehaviour
{
    
    public Direction StartingDirection;
    
    private void Awake()
    {
        Camera.main.GetComponent<CameraMove>().OnLevelEnter += EnterLevel;
    }
    
    public void EnterLevel(Transform newRoom)
    {
        if (transform.parent != newRoom)
            return;

        //Aktivera/nollställ rummet
        GameObject.Find("Player").transform.position = transform.position;
        GameObject.Find("World").GetComponent<WorldSpin>().SnapRotation(directionFromEnum(StartingDirection));
        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }

    Vector2 directionFromEnum(Direction dir)
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
