﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform currentRoom;
    Transform nextRoom;

    public float FadeOutOffset;
    public float MoveDuration;

    bool isMoving;
    float timePassed;


    private void Update()
    {
        if (!isMoving)
            return;

        timePassed += Time.unscaledDeltaTime;
        
        var lValue = Mathf.SmoothStep(0, 1, timePassed / MoveDuration);


        var newPos = Vector3.Lerp(currentRoom.position, nextRoom.position, lValue);
        newPos.z = transform.position.z;
        transform.position = newPos;

        if(timePassed >= MoveDuration)
        {
            isMoving = false;
            timePassed = 0;
            Time.timeScale = 1;
            currentRoom = nextRoom;
            nextRoom = null;
        }
    }

    public void EnterLevel(Transform targetLevel)
    {
        nextRoom = targetLevel;
        isMoving = true;
        Time.timeScale = 0;
    }
}