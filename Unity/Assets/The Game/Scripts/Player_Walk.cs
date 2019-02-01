using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    //Controls
    public string horizontalAxis;

    //Variables
    public float speed;

    //Componenets
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        ComponentSetup();
    }

    void ComponentSetup()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(Input.GetAxis(horizontalAxis) * speed, rb2D.velocity.y);
    }
}