using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : MonoBehaviour
{
    [SerializeField]
    bool CanBreakWalls;
    LayerMask playerLayer;

    [SerializeField]
    Transform Dust;

    private void Awake()
    {
        var applyGrav = GetComponent<ApplyGravity>();
        applyGrav.BeforeCollisionEnter += BeforeCollsionEnter;    
    }

    bool crushed = false;
    Player_Death death;

    void BeforeCollsionEnter(float velocity, Transform other)
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

        else
        {
            Vector3 vec = new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2, transform.position.z);
            Transform temp = Instantiate(Dust, vec, Quaternion.identity);
            temp.transform.parent = gameObject.transform;
        }
    }

    void UnCrush()
    {
        crushed = false;
        death.AfterDie -= UnCrush;
    }
}