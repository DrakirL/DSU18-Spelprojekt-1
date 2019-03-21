using UnityEngine;

public class Heavy : MonoBehaviour
{
    [SerializeField]
    private bool CanBreakWalls;
    private LayerMask playerLayer;

    [SerializeField]
    private Transform dust;

    private void Awake()
    {
        var applyGrav = GetComponent<ApplyGravity>();
        applyGrav.BeforeCollisionEnter += BeforeCollsionEnter;
    }

    private bool crushed = false;
    private Player_Death death;
    private Transform dustInstance;

    private void BeforeCollsionEnter(float velocity, Transform other)
    {
        var breakable = other.GetComponent<Breakable>();
        if (CanBreakWalls && breakable != null)
        {
            breakable.GetBroken();
        }

        if (crushed)
            return;

        if (LayerMask.GetMask("Player") == 1 << other.gameObject.layer)
        {
            death = other.GetComponent<Player_Death>();
            death.GetCrushed();
            death.AfterDie += UnCrush;
            crushed = true;
        }

        else if (GetComponent<ApplyGravity>().VelocityDown < -0.3f && dustInstance == null)
        {
            Vector3 vec = new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2, transform.position.z);
            dustInstance = Instantiate(dust, vec, Quaternion.identity);
            dustInstance.parent = gameObject.transform;

        }
    }

    private void UnCrush()
    {
        crushed = false;
        death.AfterDie -= UnCrush;
    }
}