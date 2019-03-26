using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnabler : MonoBehaviour
{
    void Awake()
    {
        DoorwayTransitions.OnEnteredDoor += EnableLevel;
        DoorwayTransitions.AfterExitedDoor += DisableOtherLevels;
    }

    void EnableLevel()
    {
        if(transform.parent == DoorwayTransitions.NextRoom)
            gameObject.SetActive(true);
    }

    void DisableOtherLevels()
    {
        gameObject.SetActive(transform.parent == DoorwayTransitions.CurrentRoom);
    }
}