using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock[] Locks;
    private Animator animator;

    [SerializeField]
    private string triggerTag = "BatteryBlock";

    [SerializeField]
    private Behaviour ActivateOnActivated;

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

            if (animator != null)
                animator.SetBool("Activated", true);
            if (ActivateOnActivated != null)
                ActivateOnActivated.enabled = true;
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

            if (ActivateOnActivated != null)
                ActivateOnActivated.enabled = false;
            if (animator != null)
                animator.SetBool("Activated", false);
        }
    }
}