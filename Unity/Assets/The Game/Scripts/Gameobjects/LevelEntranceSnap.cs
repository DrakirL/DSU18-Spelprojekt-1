using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntranceSnap : LevelEntrance
{   
    public override void StartLevel(Transform newRoom)
    {
        if (transform.parent != newRoom)
            return;

        GameObject.Find("Player").transform.position = transform.position;
        GameObject.Find("World").GetComponent<WorldSpin>().SnapRotation(directionFromEnum(StartingDirection));
        PlayerPrefs.SetString("SavedRoom", transform.parent.transform.name);
    }
}
