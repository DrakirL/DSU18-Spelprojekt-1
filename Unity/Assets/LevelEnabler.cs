using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnabler : MonoBehaviour
{
    void Awake()
    {
        Camera.main.GetComponent<CameraMove>().OnLevelEnter += EnableLevel;
    }

    void EnableLevel(Transform room)
    {
        gameObject.SetActive(transform.parent == room);
    }
}