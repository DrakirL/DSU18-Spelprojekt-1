using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : MonoBehaviour
{
    [SerializeField]
    bool CanBreakWalls;
    LayerMask playerLayer;

    private void Awake()
    {
        var applyGrav = GetComponent<ApplyGravity>();
        applyGrav.BeforeCollisionEnter += BeforeCollsionEnter;

        
    }
    bool crushed = false;
    Player_Death death;
    void BeforeCollsionEnter(float velocity, Transform other)
    {
        if (crushed)
            return;

        if (LayerMask.GetMask("Player") == 1 << other.gameObject.layer  )
        {
            death = other.GetComponent<Player_Death>();
            death.GetCrushed();
            death.AfterDie += UnCrush;
            crushed = true;
        }
    }

    void UnCrush()
    {
        crushed = false;
        death.AfterDie -= UnCrush;

    }
}