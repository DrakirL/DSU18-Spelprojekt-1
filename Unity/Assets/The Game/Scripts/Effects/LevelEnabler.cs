using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnabler : MonoBehaviour
{
    void Awake()
    {
        DoorwayTransitions.OnEnteredDoor += EnableLevel;
    }

    void EnableLevel()
    {
        gameObject.SetActive(transform.parent == DoorwayTransitions.NextRoom);
    }
}