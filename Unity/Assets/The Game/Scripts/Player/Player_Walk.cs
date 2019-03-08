using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class Player_Walk : MonoBehaviour
{
    public string HorizontalAxis;
    public float Speed;

    [Range(0, 1)]
    public float AirSpeedMultiplier;

    CharacterController2D charController;
    SpriteRenderer sr;
    Jump jump;

    [HideInInspector]
    public Vector2 input;

    bool isEnabled = true;
    private void Awake()
    {
        charController = GetComponent<CharacterController2D>();
        sr = GetComponent<SpriteRenderer>();
        jump = GetComponent<Jump>();

        var death = GetComponent<Player_Death>();
        death.BeforeDie += Disable;
        death.AfterDie += Reenable;

        DoorwayTransitions.BeforeEnteredDoor += () => Disable(CauseOfDeath.ForceReset);
        DoorwayTransitions.Done += Reenable;
    }

    void Disable(CauseOfDeath c)
    {
        isEnabled = false;
    }

    void Reenable()
    {
        isEnabled = true;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isEnabled)
            return;

        input = Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal");

        if (input.y == -1)
            input.y = 0;
        
        if (Physics2D.gravity.y != 0)
            input.y = 0;
        else
            input.x = 0;
            
        if (input == Vector2.zero)
            return;

        if (!jump.isGrounded)
        {
            charController.move(input * Speed * AirSpeedMultiplier * Time.deltaTime);
            return;
        }

        charController.move(input * Speed * Time.deltaTime);
    }
}