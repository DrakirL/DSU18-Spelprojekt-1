using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideOnAxis : MonoBehaviour
{

    public bool CollideOnY = true;
    public bool CollideOnX;

    public bool alwaysSolid;
    Collider2D col;

    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider2D>();
        GameObject.Find("World").GetComponent<WorldSpin>().OnWorldRotateTo += OnAxisChanged;
    }

    private void OnAxisChanged(Vector2 newDown)
    {
        var scale = transform.localScale;
        if (CollideOnY)
        {
            if (newDown.y == -1)
                scale.y = 1;
            else if(newDown.y == 1)
                scale.y = -1;

            col.enabled = true;
        }else if(!alwaysSolid && Mathf.Abs(newDown.y) > 0.5f)
            col.enabled = false;
        

        if (CollideOnX)
        {

            if (newDown.x == -1)
                scale.x = 1;
            else if (newDown.x == 1)
                scale.x = -1;

            col.enabled = true;
        }
        else if (!alwaysSolid && Mathf.Abs(newDown.x) > 0.5f)
            col.enabled = false;


        transform.localScale = scale;
    }

}
