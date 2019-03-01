using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform CurrentRoom => currentDoor.transform.parent;
    private Transform NextRoom => nextDoor?.transform.parent ?? null;


    [HideInInspector]
    public Doorway nextDoor;
    public Doorway currentDoor;

    public float FadeOutOffset;
    public float MoveDuration;

    bool isMoving;
    float timePassed;

    public event System.Action<Doorway> OnLevelEnter;
    public event System.Action<Doorway> AfterLevelEnter;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        var startDoorName = PlayerPrefs.GetString("SavedRoom");

        if (startDoorName != "")
        {
            var found = GameObject.Find(startDoorName).GetComponent<Doorway>();

            if (found == null)
                Debug.LogError("Couldnt find door with saved name");
            currentDoor = found;
        }

        EnterLevel(currentDoor);
    }

    private void Update()
    {
        if (!isMoving)
            return;

        timePassed += Time.unscaledDeltaTime;

        var lValue = Mathf.SmoothStep(0, 1, timePassed / MoveDuration);

        var newPos = Vector3.Lerp(CurrentRoom.position, NextRoom.position, lValue);
        newPos.z = transform.position.z;
        transform.position = newPos;

        if (timePassed >= MoveDuration)
        {
            isMoving = false;
            timePassed = 0;
            Time.timeScale = 1;
            currentDoor = nextDoor;
            nextDoor = null;
            AfterLevelEnter?.Invoke(currentDoor);
        }
    }

    public void EnterLevel(Doorway toDoor)
    {
        OnLevelEnter?.Invoke(toDoor);

        nextDoor = toDoor;

        isMoving = true;
        Time.timeScale = 0;
    }
}