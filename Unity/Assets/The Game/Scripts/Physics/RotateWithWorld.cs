using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
        GameObject.Find("World").GetComponent<WorldSpin>().OnWorldRotateBy += OnWorldRotate;
    }

    private void OnWorldRotate(float by)
    {
        transform.Rotate(0, 0, by);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
