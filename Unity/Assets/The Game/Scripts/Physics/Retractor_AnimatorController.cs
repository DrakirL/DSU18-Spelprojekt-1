using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retractor_AnimatorController : InteractableLock
{
    private Animator anim;
    bool isOpen => IsPrerequisitesFulfilled;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        OnLock += Close;
        OnUnlock += Open;
        
        DoorwayTransitions.Done += () =>
        {
            if (DoorwayTransitions.CurrentRoom != transform.parent.parent)
                return;

            if (!IsPrerequisitesFulfilled)
                Close();
        };
    }

    private void OnEnable()
    {
        anim.SetBool("IsOpen", isOpen);

        if (isOpen)
            anim.Play("Opening", 0, 1);

        else
            anim.Play("Closing", 0, 1);
    }

    private void Open()
    {
        anim.SetBool("IsOpen", true);
    }

    private void Close()
    {
        anim.SetBool("IsOpen", false);
    }
}
