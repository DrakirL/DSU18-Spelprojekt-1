using UnityEngine;

public enum DoorType
{
    Default, First, Second, Third, Fourth, Fifth
}

public class Doorway_AnimatorController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private DoorType Floor;


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
            if (DoorwayTransitions.CurrentDoor != door)
                return;
            
            if ((l != null && !l.IsPrerequisitesFulfilled) || door.Exit == null)
                Invoker.InvokeDelayed(Close, 3f);
            
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