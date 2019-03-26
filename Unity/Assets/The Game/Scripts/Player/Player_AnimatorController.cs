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

    bool noGravity;
    public bool LookingRight { get; private set; }
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

        worldSpin.BeforeWorldRotate += a => OnGravitySwitch();

        DoorwayTransitions.BeforeEnteredDoor += BeforeEnteredDoor;
        DoorwayTransitions.AfterExitedDoor += AfterExitedDoor;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWalk.input.x > 0)
            LookingRight = true;
        else if (playerWalk.input.x < 0)
            LookingRight = false;

        animator.SetBool("LookingRight", LookingRight);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", playerJump.IsJumping);

        if(!noGravity)
            animator.SetBool("IsGrounded", playerJump.IsGrounded);

        animator.SetFloat("VelY", rb2D.velocity.y);
    }

    private void OnHitGround()
    {
        noGravity = false;
        animator.SetTrigger("Landed");
        animator.SetBool("LandingIsHard", playerJump.HardLanding);

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

    public void PlayIdle()
    {
        animator.SetBool("IsMoving", false);

        if (LookingRight)
            animator.Play("PlayerIdleRight", 1, 0);

        else
            animator.Play("PlayerIdleLeft", 1, 0);

        RevertAnimator();
    }
    void RevertAnimator()
    {
        animator.updateMode = AnimatorUpdateMode.Normal;
        animator.SetLayerWeight(1, 0);
    }

    void OnGravitySwitch()
    {
        animator.ResetTrigger("Landed");
        animator.SetBool("IsGrounded", false);
        noGravity = true;

        animator.SetLayerWeight(1, 1);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        if (LookingRight)
        {
            animator.Play("PlayerNoGravityRight", 1, 0);
            animator.Play("PlayerFallingRight", 0, 0);
        }

        else
        {
            animator.Play("PlayerNoGravityLeft", 1, 0);
            animator.Play("PlayerFallingLeft", 0, 0);
        }

        worldSpin.FinishBeforeRotate();

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoker.InvokeDelayed(RevertAnimator, len);
    }

    void BeforeEnteredDoor()
    {
        //TODO center player on door
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetLayerWeight(1, 1);
        animator.Play("PlayerEnterDoor", 1, 0);

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoker.InvokeDelayed(FinishedBeforeEntered, len);
    }
    void FinishedBeforeEntered()
    {
        DoorwayTransitions.FinishBeforeEnter();
    }

    void AfterExitedDoor()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetLayerWeight(1, 1);
        animator.Play("PlayerExitDoor", 1, 0);

        var len = animator.GetCurrentAnimatorStateInfo(1).length;
        Invoker.InvokeDelayed(FinishedAfterEntered, len);
    }
    void FinishedAfterEntered()
    {
        RevertAnimator();
        DoorwayTransitions.FinishAfterEnter();
        animator.updateMode = AnimatorUpdateMode.Normal;
    }
}