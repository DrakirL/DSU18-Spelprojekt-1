using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCoverDoor : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    LevelResetter resetter;

    public float Duration;
    public float Distance;

    float startX;
    float endX;
    float durationPassed;

    bool isLerping;

    private void Awake()
    {
        resetter = Camera.main.GetComponent<LevelResetter>();
        resetter.BeforeLevelReset += CoverScreen;
        resetter.AfterResetLevel += t => {
            SetDoorVisibility(false);
            UncoverScreen();
        };

        SetDoorVisibility(false);
    }

    private void Start()
    {
        LeftDoor.transform.localPosition = new Vector3(-Distance, LeftDoor.transform.localPosition.y, LeftDoor.transform.localPosition.z);
        RightDoor.transform.localPosition = new Vector3(Distance, LeftDoor.transform.localPosition.y, LeftDoor.transform.localPosition.z);
    }

    void Update()
    {
        if (!isLerping)
            return;

        if(LeftDoor == null)
        Debug.Log(transform.name);

        durationPassed += Time.unscaledDeltaTime;
        float newPosition = Mathf.Lerp(startX, endX, durationPassed / Duration);

        LeftDoor.transform.localPosition = new Vector3(-newPosition, LeftDoor.transform.localPosition.y, LeftDoor.transform.localPosition.z);
        RightDoor.transform.localPosition = new Vector3(newPosition, LeftDoor.transform.localPosition.y, LeftDoor.transform.localPosition.z);

        if (durationPassed >= Duration)
        {
            isLerping = false;
            durationPassed = 0;
            if (endX == 0)
                resetter.FinishResetLevel();
            
        }
    }

    void CoverScreen()
    {
        SetDoorVisibility(true);
        isLerping = true;
        startX = Distance;
        endX = 0;
    }

    void UncoverScreen()
    {
        SetDoorVisibility(true);
        isLerping = true;
        startX = 0;
        endX = Distance;
    }

    void SetDoorVisibility(bool b)
    {
        LeftDoor.GetComponent<SpriteRenderer>().enabled = b;
        RightDoor.GetComponent<SpriteRenderer>().enabled = b;
    }
}