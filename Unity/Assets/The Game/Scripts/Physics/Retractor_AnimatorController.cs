using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retractor_AnimatorController : InteractableLock
{
    private Animator anim;
    bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        OnLock += Close;
        OnUnlock += Open;
        
        Open();

        var door = GetComponent<Doorway>();

        DoorwayTransitions.Done += () =>
        {
            if (DoorwayTransitions.CurrentRoom != door.Room)
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
        isOpen = true;
        anim.SetBool("IsOpen", true);
    }

    private void Close()
    {
        isOpen = false;
        anim.SetBool("IsOpen", false);
    }
}
