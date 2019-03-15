using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLock : Interactable
{
    [SerializeField]
    int prerequisites;

    int prFulfilled;
    
    public int PrerequisitesFulfilled
    {
        get => prFulfilled;
        set {

            if (prFulfilled >= prerequisites && value < prerequisites)
            {
                OnLock();
            }

            if (prFulfilled < prerequisites && value >= prerequisites)
                OnUnlock();

            prFulfilled = value;
        }
    }

    public bool IsPrerequisitesFulfilled => prerequisites <= PrerequisitesFulfilled;

    public event System.Action OnUnlock;
    public event System.Action OnLock;

    public override void Interact(GameObject obj)
    {
        if (IsPrerequisitesFulfilled)
        {
            base.Interact(obj);
        }
    }
}