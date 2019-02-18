using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntranceSnap : Doorway
{

    public override void ExitRoom()
    {
        Vector2 currentDown = GameObject.Find("World").transform.rotation * Vector2.down;
        var orientation = directionFromEnum(Orientation);

        if (orientation != currentDown)
            return;

        base.ExitRoom();
    }

    protected override void StartRoom(Transform newRoom)
    {
        if (Room != newRoom)
            return;

        
        var newDir = directionFromEnum(Orientation);


        GameObject.Find("Player").transform.position = transform.position;

        GameObject.Find("World").GetComponent<WorldSpin>().SnapRotation(newDir);

        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }
}
