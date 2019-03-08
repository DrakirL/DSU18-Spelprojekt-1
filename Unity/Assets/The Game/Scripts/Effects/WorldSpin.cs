﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpin : MonoBehaviour
{
    GameObject Player;
    CameraMove cameraMove;

    [SerializeField]
    float startRotationDelay;

    [SerializeField]
    float endRotationDelay;

    Vector2 input;

    [HideInInspector]
    public bool isRotating;

    float startRotation;
    float endRotation;

    [SerializeField]
    float defaultRotationDuration;

    float rotationDuration;

    float rotationDurationPassed;

    public event Action BeforeWorldRotate;
    public event Action<float> OnWorldRotateBy;
    public event Action<Vector2> OnWorldRotateTo;

    bool runMethod;
    Action<object> toRun;
    float inTime;
    object parameter;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        cameraMove = Camera.main.GetComponent<CameraMove>();

        OmniDisabler.SetActiveBasedOnEnable(this);
    }

    void SetMethodToRun(Action<object> a, float inTime, object param)
    {
        runMethod = true;
        this.inTime = inTime;
        toRun = a;
        parameter = param;
    }

    // Update is called once per frame
    void Update()
    {
        if (runMethod)
        {
            inTime -= Time.unscaledDeltaTime;
            if (inTime <= 0)
            {
                inTime = 0;
                runMethod = false;
                toRun(parameter);
                parameter = null;
            }
        }

        NonInstantUpdate();
    }

    float lastRotation = 0f;
    void NonInstantUpdate()
    {
        if (!isRotating)
        {
            input = new Vector2(Input.GetAxisRaw("FlipX"), Input.GetAxisRaw("FlipY"));

            if (input.y == -1)
                input.y = 0;

            if (input == Vector2.zero)
                return;

            if (input.x != 0)
                input.y = 0;

            OmniDisabler.Disable();
            enabled = true;

            isRotating = true;
            BeforeWorldRotate?.Invoke();
        }

        if (isRotating && rotationDurationPassed < rotationDuration)
        {

            rotationDurationPassed += Time.unscaledDeltaTime;
            float newRotation = Mathf.Lerp(startRotation, endRotation, rotationDurationPassed / rotationDuration);


            transform.RotateAround(DoorwayTransitions.CurrentRoom.position, Vector3.forward, newRotation - lastRotation);


            OnWorldRotateBy?.Invoke(lastRotation - newRotation);

            lastRotation = newRotation;

            if (rotationDurationPassed >= rotationDuration)
                StopLerp();
        }
    }
    public void FinishBeforeRotate()
    {
        StartLerp(input);
    }

    public void SnapRotation(Vector2 newDown)
    {

        if (newDown.y == -1)
            newDown.x = 0;
        else if (newDown.y == 1)
            newDown.x = 2;

        startRotation = transform.rotation.eulerAngles.z;
        endRotation = -90 * newDown.x;

        transform.RotateAround(DoorwayTransitions.CurrentRoom.position, Vector3.forward, endRotation - startRotation);

        OnWorldRotateTo?.Invoke(newDown);
        OnWorldRotateBy?.Invoke(lastRotation - endRotation);

        lastRotation = endRotation;
    }

    void StartLerp(object param)
    {
        Vector2 flipDirection = (Vector2)param;

        rotationDuration = Mathf.Abs(defaultRotationDuration * flipDirection.x + defaultRotationDuration * flipDirection.y * 0.5f);
        flipDirection.x += flipDirection.y * 2;

        Vector2 currentDown = transform.rotation * Vector2.down;

        startRotation = transform.rotation.eulerAngles.z;
        endRotation = startRotation - 90 * flipDirection.x;

        rotationDurationPassed = 0f;

        OnWorldRotateTo?.Invoke(Quaternion.Euler(0, 0, endRotation) * Vector2.down);
    }

    void StopLerp()
    {
        isRotating = false;
        SetMethodToRun(ResetTimeScale, endRotationDelay, null);
    }

    void ResetTimeScale(object param)
    {
        OmniDisabler.Enable();
    }
}