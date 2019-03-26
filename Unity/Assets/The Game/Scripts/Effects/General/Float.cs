using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField]
    Vector2 Offset;

    Vector2 startPos;
    Vector2 endPos;

    [SerializeField]
    float Duration;

    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = (Vector2)transform.position + Offset;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        var lValue = Mathf.SmoothStep(0, 1, timePassed / Duration);

        var newPos = Vector3.Lerp(startPos, endPos, lValue);
        newPos.z = transform.position.z;

        transform.position = newPos;

        if(timePassed >= Duration)
        {
            timePassed = 0;

            var temp = startPos;
            startPos = endPos;
            endPos = temp;
        }
    }
}