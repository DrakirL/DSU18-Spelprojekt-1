using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLock : Interactable
{
    public int prerequisites;
    public int prerequisitesMet;
    
    public override void Interact(GameObject obj)
    {
        if (prerequisitesMet >= prerequisites)
        {
        base.Interact(obj);
        }
    }

}
