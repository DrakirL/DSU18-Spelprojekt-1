using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock Lock;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "BatteryBlock")
        {
            Lock.PrerequisitesFulfilled++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BatteryBlock")
        {
            Lock.PrerequisitesFulfilled--;
        }
    }
}
