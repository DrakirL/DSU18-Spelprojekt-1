using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpin : MonoBehaviour
{
    [HideInInspector]
    public bool isRotating;

    Quaternion startRotation;
    Quaternion endRotation;

    [SerializeField]
    float rotationDuration;

    float rotationDurationPassed;


    private void Start()
    {
    }





    // Update is called once per frame
    void Update()
    {

        if (!isRotating)
        {
            var input = new Vector2(Input.GetAxisRaw("FlipX"), Input.GetAxisRaw("FlipY"));
            if (input == Vector2.zero)
                return;

            if (input.x != 0)
                input.y = 0;

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


    void StartLerp(Vector2 newRotation)
    {
        //Should never happen
        if (isRotating)
            return;

        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, newRotation));
        rotationDurationPassed = 0f;
        Time.timeScale = 0;
        isRotating = true;

    }

    void StopLerp()
    {
        Time.timeScale = 1;
        isRotating = false;

    }
}
