﻿using System.Collections;
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
    bool isJumping => playerJump.isJumping;
    bool landingIsHard => playerJump.HardLanding;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerWalk = GetComponent<Player_Walk>();
        playerJump = GetComponent<Jump>();
        playerDeath = GetComponent<Player_Death>();

        playerJump.HitGround += OnHitGround;
        playerDeath.BeforeDie += OnDeath;
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
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsGrounded", playerJump.isGrounded);
        animator.SetFloat("VelY", rb2D.velocity.y);
        animator.SetBool("LandingIsHard", landingIsHard);
    }

    private void OnHitGround()
    {
        animator.SetTrigger("Landed");
    }
    void OnDeath(CauseOfDeath cause)
    {
        switch (cause)
        {
            case CauseOfDeath.Touched:
                animator.SetTrigger("DeathTouched");
                break;

            case CauseOfDeath.Crushed:
                animator.SetTrigger("Crushed");
                break;

            case CauseOfDeath.OutOfBounds:
                animator.SetTrigger("FellOutOfBounds");
                break;
        }
    }
}