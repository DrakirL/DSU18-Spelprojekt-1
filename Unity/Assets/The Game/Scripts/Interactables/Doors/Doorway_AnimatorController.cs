using UnityEngine;

public class Doorway_AnimatorController : MonoBehaviour
{
    private Animator anim;
    bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        var l = GetComponent<InteractableLock>();

        if (l != null)
        {
            l.OnLock += Close;
            l.OnUnlock += Open;
        }

        else
            Open();

        var door = GetComponent<Doorway>();
        
        DoorwayTransitions.Done += () =>
        {
            if (DoorwayTransitions.CurrentRoom != door.Room)
                return;

            if ((l != null && !l.IsPrerequisitesFulfilled) || door.Exit == null)
                Close();
        };

        DoorwayTransitions.OnEnteredDoor += () =>
        {
            if(DoorwayTransitions.CurrentDoor == gameObject.GetComponent<Doorway>())
                anim.Play("Door Opening", 0, 1);
        };
    }

    private void OnEnable()
    {
        anim.SetBool("IsOpen", isOpen);

        if(isOpen)
            anim.Play("Door Opening", 0, 1);

        else
            anim.Play("Door Closing", 0, 1);
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