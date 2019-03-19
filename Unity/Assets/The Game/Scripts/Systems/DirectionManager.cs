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

public static class DirectionManager
{
    public static Direction CurrentDirection;

    public static Vector2 VectorFromDirection(Direction dir)
    {
        var vec = Vector2.down;

        switch(dir)
        {
            case Direction.DOWN:
                vec = Vector2.down;
                break;

            case Direction.UP:
                vec = Vector2.up;
                break;

            case Direction.LEFT:
                vec = Vector2.left;
                break;

            case Direction.RIGHT:
                vec = Vector2.right;
                break;
        }

        return vec;
    }
    public static Vector2 CurrentDownVector { get => VectorFromDirection(CurrentDirection); }

    public static float RotationValueFromDirection(Direction dir)
    {
        float rot = 0;

        switch (dir)
        {
            case Direction.DOWN:
                rot = 0;
                break;

            case Direction.UP:
                rot = 180;
                break;

            case Direction.LEFT:
                rot = 270;
                break;

            case Direction.RIGHT:
                rot = 90;
                break;
        }

        return rot;
    }
    public static float CurrentRotationRotation { get => RotationValueFromDirection(CurrentDirection); }

    public static Direction DirectionFromVector(Vector2 vec)
    {
        Direction dir = Direction.DOWN;

        if (vec == Vector2.down)
            dir = Direction.DOWN;

        else if (vec == Vector2.up)
            dir = Direction.UP;

        else if (vec == Vector2.left)
            dir = Direction.LEFT;

        else if (vec == Vector2.right)
            dir = Direction.RIGHT;

        return dir;
    }
    public static Direction DirectionFromRotationValue(float rot)
    {
        Direction dir = Direction.DOWN;

        if (rot >= -45 && rot <= 45)
            dir = Direction.DOWN;

        else if (rot >= 45 && rot <= 135)
            dir = Direction.RIGHT;

        else if (rot >= 135 && rot <= 225)
            dir = Direction.UP;

        else if (rot >= 225 && rot <= 315)
            dir = Direction.LEFT;

        return dir;
    }
}