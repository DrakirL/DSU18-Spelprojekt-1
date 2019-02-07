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

    [HideInInspector]
    public bool isRotating;

    float startRotation;
    float endRotation;

    [SerializeField]
    float defaultRotationDuration;

    float rotationDuration;

    float rotationDurationPassed;

    public event Action<float> OnWorldRotateBy;
    public event Action<Vector2> OnWorldRotateTo;



    bool runMethod;
    Action<object> toRun;
    float inTime;
    object parameter;

    [SerializeField]
    CameraMove cameraMove;

    void SetMethodToRun(Action<object> a, float inTime,object param)
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
            if(inTime <= 0)
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
            var input = new Vector2(Input.GetAxisRaw("FlipX"), Input.GetAxisRaw("FlipY"));

            if (input.y == -1)
                input.y = 0;

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
            float newRotation = Mathf.Lerp(startRotation, endRotation, rotationDurationPassed / rotationDuration);


            transform.RotateAround(cameraMove.currentRoom.position, Vector3.forward, newRotation - lastRotation);


            OnWorldRotateBy?.Invoke(lastRotation- newRotation);

            lastRotation = newRotation;

            if (rotationDurationPassed >= rotationDuration)
                StopLerp();
        }
    }

    public void SnapRotation(Vector2 newDown)
    {
        startRotation = transform.rotation.eulerAngles.z;
        endRotation = -90 * newDown.x;

        transform.RotateAround(cameraMove.currentRoom.position, Vector3.forward, endRotation - startRotation);

        OnWorldRotateTo?.Invoke(newDown);
        OnWorldRotateBy?.Invoke(lastRotation - endRotation);

        lastRotation = endRotation;
    }

    void StartLerp(object param)
    {
        Vector2 flipDirection = (Vector2)param;


        rotationDuration = Mathf.Abs(defaultRotationDuration * flipDirection.x + defaultRotationDuration * flipDirection.y * 0.5f);
        flipDirection.x += flipDirection.y*2;

        //Should never happen
        if (isRotating)
            return;

        Vector2 currentDown = transform.rotation * Vector2.down;
        
        startRotation = transform.rotation.eulerAngles.z;
        endRotation = startRotation - 90 * flipDirection.x;

        rotationDurationPassed = 0f;
        isRotating = true;

        OnWorldRotateTo?.Invoke(Quaternion.Euler(0, 0, endRotation) * Vector2.down);

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
