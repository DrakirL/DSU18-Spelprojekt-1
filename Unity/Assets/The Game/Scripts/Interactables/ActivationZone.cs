using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock[] Locks;
    private Animator animator;

    [SerializeField]
    private string triggerTag = "BatteryBlock";

    private void Start() => animator = GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            foreach (var l in Locks)
            {
                if (l != null)
                    l.PrerequisitesFulfilled++;
            }

            animator.SetBool("Activated", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            foreach (var l in Locks)
            {
                if (l != null)
                    l.PrerequisitesFulfilled--;
            }

            animator.SetBool("Activated", false);
        }
    }
}