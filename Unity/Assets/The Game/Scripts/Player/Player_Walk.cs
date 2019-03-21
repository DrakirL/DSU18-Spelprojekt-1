﻿using System;
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

    private void OnDisable()
    {
        input = Vector2.zero;
    }



    private void Awake()
    {
        OmniDisabler.SetActiveBasedOnEnable(this);

        charController = GetComponent<CharacterController2D>();
        sr = GetComponent<SpriteRenderer>();
        jump = GetComponent<Jump>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        input = Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal");

        if (input.y == -1)
            input.y = 0;
        
        if (Physics2D.gravity.y != 0)
            input.y = 0;
        else
            input.x = 0;
            
        if (input == Vector2.zero)
            return;

        if (!jump.IsGrounded)
        {
            charController.move(input * Speed * AirSpeedMultiplier * Time.deltaTime);
            return;
        }

        charController.move(input * Speed * Time.deltaTime);
    }
}