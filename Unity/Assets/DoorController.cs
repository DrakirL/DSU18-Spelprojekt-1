using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Sprite OpenDoor;
    bool open;
    CameraMove move;

    // Start is called before the first frame update
    void Start()
    {
        move = Camera.main.GetComponent<CameraMove>();
        move.OnLevelEnter += setAnim;
    }

    void setAnim(Doorway doorway)
    {
        var thisDoorway = GetComponent<Doorway>();
        var sr = GetComponent<SpriteRenderer>();
        var animator = GetComponent<Animator>();

        if (doorway == thisDoorway && !open)
        {
            animator.enabled = false;
            sr.sprite = OpenDoor;
            open = true;
        }

        animator.SetBool("Open", open);
    }

    public void PlayOpen()
    {
        if (open)
            return;

        var animator = GetComponent<Animator>();
        animator.enabled = true;
        open = true;
        animator.SetBool("Open", open);
    }

}
