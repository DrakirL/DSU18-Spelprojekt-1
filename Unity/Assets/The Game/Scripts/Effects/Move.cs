using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    Vector2[] Points;

    [SerializeField]
    float Speed;

    int nextPointIndex;
    float measuredDuration;
    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Points[0];
        nextPointIndex = 1;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= measuredDuration)
        {
            DetermineNext();
        }
    }

    void DetermineNext()
    {
        int temp = nextPointIndex + 1;

        if (temp >= Points.Length)
            nextPointIndex = 0;

        measuredDuration = Speed / Vector2.Distance(Points[nextPointIndex], Points[temp]);

        nextPointIndex = temp;
        timePassed = 0;
    }
}