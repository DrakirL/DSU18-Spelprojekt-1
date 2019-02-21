using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLock : Interactable
{
    [SerializeField]
    int prerequisites;

    [SerializeField]
    private LevelResetter resetter;
    
    public int PrerequisitesFulfilled { get; set; }

    public bool IsPrerequisitesFulfilled() 
        => prerequisites <= PrerequisitesFulfilled;

    public override void Interact(GameObject obj)
    {
        if (IsPrerequisitesFulfilled())
        {
            base.Interact(obj);
        }
    }
}