using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player_AnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2D;
    Player_Walk playerWalk;
    Jump playerJump;
    Player_Death playerDeath;

    bool lookingRight;
    bool isMoving => playerWalk.input != Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerWalk = GetComponent<Player_Walk>();
        playerJump = GetComponent<Jump>();
        playerDeath = GetComponent<Player_Death>();

        playerJump.HitGround += OnHitGround;
        playerDeath.BeforeDie += OnDeath;
        playerDeath.AfterDie += OnRespawn;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerWalk.input.x > 0)
            lookingRight = true;
        else if (playerWalk.input.x < 0)
            lookingRight = false;

        animator.SetBool("LookingRight", lookingRight);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", playerJump.isJumping);
        animator.SetBool("IsGrounded", playerJump.isGrounded);
        animator.SetFloat("VelY", rb2D.velocity.y);
        animator.SetBool("LandingIsHard", playerJump.HardLanding);
    }

    private void OnHitGround()
    {
        animator.SetTrigger("Landed");
    }

    void OnDeath(CauseOfDeath cause)
    {
        animator.SetLayerWeight(1, 1);

        switch (cause)
        {
            case CauseOfDeath.OutOfBounds:
                animator.SetTrigger("FellOutOfBounds");
                break;

            default:
                animator.SetTrigger("Poofed");
                break;
        }
    }

    void OnRespawn()
    {
        animator.SetLayerWeight(1, 0);

        animator.SetTrigger("Respawned");
    }
}