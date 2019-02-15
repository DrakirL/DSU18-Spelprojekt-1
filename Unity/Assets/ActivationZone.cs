using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock Lock;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "BatteryBlock")
        {
            Lock.PrerequisitesFulfilled++;
            animator.SetBool("Activated", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BatteryBlock")
        {
            Lock.PrerequisitesFulfilled--;
            animator.SetBool("Activated", false);
        }
    }
}