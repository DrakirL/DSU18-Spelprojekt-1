using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator animator;
    bool isOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeState(bool newState)
    {
        isOpen = newState;
        animator.SetBool("Open", isOpen);
    }

    public void OnEnable()
    {
        ChangeState(isOpen);
    }
}