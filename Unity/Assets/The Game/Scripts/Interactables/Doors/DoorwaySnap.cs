using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwaySnap : Doorway
{
    Direction Orientation;

    protected new void Awake()
    {
        Orientation = DirectionManager.DirectionFromRotationValue(transform.rotation.eulerAngles.z);
        base.Awake();
    }

    public override void ExitRoom()
    {
        if (Exit == null)
            return;

        Vector2 currentDown = GameObject.Find("World").transform.rotation * Vector2.down;
        currentDown.x = -currentDown.x;
        
        Debug.Log("orientation: " + currentDown);
        Debug.Log("currentDown: " + currentDown);

        Debug.Log(Orientation != DirectionManager.DirectionFromVector(currentDown));

        if (Orientation != DirectionManager.DirectionFromVector(currentDown))
            return;

        base.ExitRoom();
    }

    protected override void StartRoom(Doorway door)
    {
        if (this != door)
            return;
        
        var newDir = DirectionManager.VectorFromDirection(Orientation);

        GameObject.Find("Player").transform.position = transform.position;

        GameObject.Find("World").GetComponent<WorldSpin>().SnapRotation(newDir);

        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }
}