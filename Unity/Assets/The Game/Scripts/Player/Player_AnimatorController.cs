﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2D;
    Player_Walk playerWalk;
    Jump playerJump;

    bool lookingRight;
    bool isMoving => playerWalk.input != Vector2.zero;    
    bool isJumping => rb2D.velocity.y > 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerWalk = GetComponent<Player_Walk>();
        playerJump = GetComponent<Jump>();
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
    }
}