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


    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController2D>();
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        var input = Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal");
        
        if (Physics2D.gravity.y != 0)
            input.y = 0;
        else
            input.x = 0;
            
        if (input == Vector2.zero)
            return;

        charController.move(input * speed * Time.deltaTime);
    }
}