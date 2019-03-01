using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwaySnap : Doorway
{
    Direction Orientation;
    Direction enumFromRotation(float z)
    {
        if (z == 90)
            return Direction.RIGHT;

        else if (z == 180)
            return Direction.UP;

        else if (z == 270)
            return Direction.LEFT;

        else
            return Direction.DOWN;
    }

    protected new void Awake()
    {
        Orientation = enumFromRotation(transform.rotation.eulerAngles.z);
        base.Awake();
    }

    public override void ExitRoom()
    {
        if (Exit == null)
            return;

        Vector2 currentDown = GameObject.Find("World").transform.rotation * Vector2.down;
        currentDown.x = -currentDown.x;

        var orientation = directionFromEnum(Orientation);

        if (orientation != currentDown)
            return;

        base.ExitRoom();
    }

    protected override void StartRoom(Doorway door)
    {
        if (this != door)
            return;
        
        var newDir = directionFromEnum(Orientation);

        GameObject.Find("Player").transform.position = transform.position;

        GameObject.Find("World").GetComponent<WorldSpin>().SnapRotation(newDir);

        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }
}