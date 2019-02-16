using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock[] Locks;
    Animator animator;

    [SerializeField]
    string triggerTag = "BatteryBlock";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            foreach (var l in Locks )
                l.PrerequisitesFulfilled++;

            animator.SetBool("Activated", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            foreach (var l in Locks)
                l.PrerequisitesFulfilled--;

            animator.SetBool("Activated", false);
        }
    }
}