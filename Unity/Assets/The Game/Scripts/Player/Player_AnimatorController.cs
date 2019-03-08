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
    WorldSpin worldSpin;

    bool lookingRight;
    bool isMoving => playerWalk.input != Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerWalk = GetComponent<Player_Walk>();
        playerJump = GetComponent<Jump>();
        playerDeath = GetComponent<Player_Death>();
        worldSpin = GameObject.FindObjectOfType<WorldSpin>();

        playerJump.HitGround += OnHitGround;
        playerDeath.BeforeDie += OnDeath;
        playerDeath.AfterDie += RevertAnimator;

        worldSpin.BeforeWorldRotate += OnGravitySwitch;

        DoorwayTransitions.BeforeEnteredDoor += BeforeEnteredDoor;
        DoorwayTransitions.AfterEnteredDoor += AfterEnteredDoor;
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
        animator.SetBool("IsJumping", playerJump.IsJumping);
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
            default:
                animator.SetLayerWeight(1, 1);
                animator.Play("PlayerPoofed", 1, 0);
                break;
        }
    }

    void RevertAnimator()
    {
        animator.SetLayerWeight(1, 0);
    }

    void OnGravitySwitch()
    {
        animator.ResetTrigger("Landed");
        animator.SetLayerWeight(1, 1);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        if(lookingRight)
            animator.Play("PlayerNoGravityRight", 1, 0);

        else
            animator.Play("PlayerNoGravityLeft", 1, 0);

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoker.InvokeDelayed(FinishedBeforeWorldRotate, len);
    }
    void FinishedBeforeWorldRotate()
    {
        animator.updateMode = AnimatorUpdateMode.Normal;
        animator.SetLayerWeight(1, 0);
        worldSpin.FinishBeforeRotate();
    }

    void BeforeEnteredDoor()
    {
        //TODO center player on door

        animator.SetLayerWeight(1, 1);
        animator.Play("PlayerEnterDoor", 1, 0);

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoke("FinishedBeforeEntered", len);
    }
    void FinishedBeforeEntered()
    {
        DoorwayTransitions.FinishBeforeEnter();
    }

    void AfterEnteredDoor()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("PlayerExitDoor", 1, 0);

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoke("FinishedAfterEntered", len);
    }
    void FinishedAfterEntered()
    {
        animator.SetLayerWeight(1, 0);
        DoorwayTransitions.FinishAfterEnter();
    }
}