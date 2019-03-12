using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float FadeOutOffset;
    public float MoveDuration;

    bool isMoving;
    float timePassed;

    Doorway firstDoor;

    private void Start()
    {
        //TODO Change abstraction of doors to rooms?

        DoorwayTransitions.OnEnteredDoor += MoveToRoom;

        firstDoor = GameObject.FindGameObjectWithTag("First Door").GetComponent<Doorway>();

        var startRoomName = PlayerPrefs.GetString("SavedRoom");

        if (startRoomName != "")
        {
            var startDoorName = PlayerPrefs.GetString("SavedDoor");

            if (startDoorName != "")
            {
                var found = GameObject.Find(startDoorName)?.GetComponent<Doorway>();

                if (found == null)
                    Debug.LogError("Couldnt find door with the saved name: " + startDoorName);
                else
                    firstDoor = found;
            }
            else
                Debug.Log("couldnt find saved door");
        }
        else
            Debug.Log("couldnt find saved room: " + startRoomName);

        if (firstDoor == null)
            Debug.LogError("No first door saved, using Serialized door.");

        DoorwayTransitions.Start(firstDoor);
    }

    private void Update()
    {
        if (!isMoving)
            return;

        timePassed += Time.unscaledDeltaTime;

        var lValue = Mathf.SmoothStep(0, 1, timePassed / MoveDuration);

        var newPos = Vector3.Lerp(DoorwayTransitions.CurrentRoom.position, DoorwayTransitions.NextRoom.position, lValue);
        newPos.z = transform.position.z;
        transform.position = newPos;

        if (timePassed >= MoveDuration)
        {
            isMoving = false;
            timePassed = 0;

            DoorwayTransitions.FinishMoveToRoom();
        }
    }
    
    void MoveToRoom()
    {
        isMoving = true;
    }
}