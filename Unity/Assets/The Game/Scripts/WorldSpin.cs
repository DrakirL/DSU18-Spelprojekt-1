using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpin : MonoBehaviour
{
    [SerializeField]
    float startRotationDelay;

    [SerializeField]
    float endRotationDelay;


    [SerializeField]
    bool Instant = false;


    [HideInInspector]
    public bool isRotating;

    Quaternion startRotation;
    Quaternion endRotation;

    [SerializeField]
    float defaultRotationDuration;

    float rotationDuration;

    float rotationDurationPassed;


    bool runMethod;
    Action<object> toRun;
    float inTime;
    object parameter;

    void SetMethodToRun(Action<object> a, float inTime,object param)
    {
        runMethod = true;
        this.inTime = inTime;
        toRun = a;
        parameter = param;
        Debug.Log("Set method");

    }


    // Update is called once per frame
    void Update()
    {
        if (runMethod)
        {
            inTime -= Time.unscaledDeltaTime;
            if(inTime <= 0)
            {
                inTime = 0;
                runMethod = false;
                toRun(parameter);
                parameter = null;
                Debug.Log("Ran method");
            }
        }


        if (!Instant)
            NonInstantUpdate();
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);



    }


    void NonInstantUpdate()
    {
        if (!isRotating)
        {
            var input = new Vector2(Input.GetAxisRaw("FlipX"), Input.GetAxisRaw("FlipY"));
            if (input == Vector2.zero)
                return;

            if (input.x != 0)
                input.y = 0;

            Time.timeScale = 0;
            StartLerp(input);
        }


        if (isRotating && rotationDurationPassed < rotationDuration)
        {
            rotationDurationPassed += Time.unscaledDeltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationDurationPassed / rotationDuration);

            if (rotationDurationPassed >= rotationDuration)
                StopLerp();
        }
    }


    void StartLerp(object param)
    {
        Vector2 newDown = (Vector2)param;

        Debug.Log(newDown);
        rotationDuration = Mathf.Abs(defaultRotationDuration * newDown.x + defaultRotationDuration * newDown.y * 0.5f);
        Debug.Log(rotationDuration);

        newDown.x += newDown.y*2;

        //Should never happen
        if (isRotating)
            return;

        Vector2 currentDown = transform.rotation * Vector2.down;
        

        startRotation = transform.rotation;
        endRotation = transform.rotation * Quaternion.Euler(0, 0, 90 * newDown.x);
        rotationDurationPassed = 0f;
        isRotating = true;

    }

    void StopLerp()
    {
        isRotating = false;
        SetMethodToRun(ResetTimeScale, endRotationDelay, null);
    }

    void ResetTimeScale(object param)
    {
        Time.timeScale = 1;
    }
}
