﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    bool canBeBrokenByPlayer;

    public bool CanBeBrokenByPlayer => canBeBrokenByPlayer;

    void Awake()
    {
        Camera.main.GetComponent<LevelResetter>().AfterResetLevel += OnReset;
    }

    public void GetBroken()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void OnReset(Doorway door)
    {
        if (transform.parent.parent != door.Room)
            return;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}