using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    Vector2[] Points;

    [SerializeField]
    float Speed;

    int targetPointIndex;
    int nextPointIndex
    {
        get
        {
            var temp = targetPointIndex + 1;

            if (temp < Points.Length)
                return temp;

            else
                return 0;
        }
    }

    float measuredDuration;
    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        targetPointIndex = 0;
        transform.localPosition = Points[targetPointIndex];
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        var lValue = Mathf.Lerp(0, 1, timePassed / measuredDuration);

        var newPos = Vector3.Lerp(Points[targetPointIndex], Points[nextPointIndex], lValue);
        newPos.z = transform.localPosition.z;

        transform.localPosition = newPos;

        if (timePassed >= measuredDuration)
        {
            DetermineNext();
        }
    }

    void DetermineNext()
    {
        targetPointIndex = nextPointIndex;

        measuredDuration = Vector2.Distance(Points[targetPointIndex], Points[nextPointIndex]) / Speed;
        
        timePassed = 0;
    }
}