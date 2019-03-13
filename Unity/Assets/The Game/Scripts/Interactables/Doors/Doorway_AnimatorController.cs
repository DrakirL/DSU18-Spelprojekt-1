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
    private bool isOpen;

    private string StringFromType(string phrase)
    {
        string s = "";

        if (Floor != DoorType.Default)
            s = Floor + " " + "Floor " + phrase;

        else
            s = phrase;

        Debug.Log(s);

        return s;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        var l = GetComponent<InteractableLock>();
        if (l == null)
            Debug.Log(transform.name);

        if (l != null)
        {
            l.OnLock += Close;
            l.OnUnlock += Open;
        }

    }
    

    /*
     void UpdateState(bool open, bool instant)
    {
        isOpen = open;
        animator.SetBool("isOpen", isOpen);
    }
    */
    private void Open() => animator.Play(StringFromType("Door Opening"), 1, 0);

    private void Close() => animator.Play(StringFromType("Door Closing"), 1, 0);
}