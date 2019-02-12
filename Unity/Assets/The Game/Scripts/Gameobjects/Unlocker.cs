using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public InteractableLock Lock;
    private bool active;

    public void togglePrerequisite()
    {
        active = !active;
        if (active)
        {
            Lock.prerequisitesMet++;
        }
        else
        {
            Lock.prerequisitesMet--;
        }
    }
}
