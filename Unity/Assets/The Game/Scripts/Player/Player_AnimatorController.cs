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

        playerJump.HitGround += OnHitGround;
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
}