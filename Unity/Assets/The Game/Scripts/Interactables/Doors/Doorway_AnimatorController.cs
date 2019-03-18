using UnityEngine;

public class Doorway_AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        var l = GetComponent<InteractableLock>();

        if (l != null)
        {
            l.OnLock += Close;
            l.OnUnlock += Open;
        }

        var door = GetComponent<Doorway>();
        
        DoorwayTransitions.Done += () =>
        {
            if (DoorwayTransitions.CurrentRoom != door.Room)
                return;

            if ((l != null && !l.IsPrerequisitesFulfilled) || door.Exit == null)
                Close();      
        };
    }

    private void Open()
    {
        animator.SetBool("IsOpen", true);
    }

    private void Close()
    {
        animator.SetBool("IsOpen", false);
    }
}