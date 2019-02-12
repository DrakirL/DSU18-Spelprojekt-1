using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2D;
    Player_Walk playerWalk;
    Jump playerJump;

    bool lookingRight
    {
        get
        {
            return playerWalk.input.x > 0;
        }
    }
    bool lookingLeft
    {
        get
        {
            return playerWalk.input.x < 0;
        }
    }
    bool isMoving
    {
        get
        {
            return !Mathf.Approximately(rb2D.velocity.x, 0);
        }
    }
    bool isJumping
    {
        get
        {
            return rb2D.velocity.y > 0;
        }
    }

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
        animator.SetBool("LookingRight", lookingRight);
        animator.SetBool("LookingLeft", lookingLeft);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsGrounded", playerJump.isGrounded);
    }
}