using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public InteractableLock[] Locks;
    private Animator animator;

    [SerializeField]
    private string triggerTag = "BatteryBlock";

    int activators;
    bool activated = false;

    private void Start() => animator = GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            activators++;

            if(!activated)
            {
                activated = true;

                foreach (var l in Locks)
                {
                    if (l != null)
                        l.PrerequisitesFulfilled++;
                }

                animator.SetBool("Activated", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == triggerTag)
        {
            activators--;

            if (activators <= 0)
            {
                activated = false;

                foreach (var l in Locks)
                {
                    if (l != null)
                        l.PrerequisitesFulfilled--;
                }

                animator.SetBool("Activated", false);
            }
        }
    }

    private void OnDisable()
    {
        activators = 0;
    }

    private void OnEnable()
    {
        animator.SetBool("Activated", activated);
    }
}