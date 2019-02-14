using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class Player_Walk : MonoBehaviour
{
    public string horizontalAxis;
    public float speed;

    CharacterController2D charController;
    SpriteRenderer sr;

    [HideInInspector]
    public Vector2 input;

    bool isEnabled = true;
    private void Awake()
    {
        var death = GetComponent<Player_Death>();
        death.BeforeDie += Disable;
        death.AfterDie += Reenable;
    }

    void Disable()
    {
        isEnabled = false;
    }

    void Reenable()
    {
        isEnabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        charController = GetComponent<CharacterController2D>();
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

        charController.move(input * speed * Time.deltaTime);
    }
}