using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public InteractableLock[] Locks;
    private bool active;

    [SerializeField]
    private LevelResetter resetter;

    private void Awake()
    {
        resetter.AfterResetLevel += t =>
        {
            active = false;
            ResetAnim();
        };
        ResetAnim();
        
    }

    public void togglePrerequisite()
    {
        active = !active;
        var v = 1;
        if (!active)
            v = -1;

        foreach (var l in Locks)
            l.PrerequisitesFulfilled += v;
    }

    void ResetAnim()
    {
        var anim = GetComponent<Animator>();

        if (active)
            anim.Play("PullLeverDown", 0, 1);

        if (!active)
            anim.Play("PullLeverUp", 0, 1);
    }
}
