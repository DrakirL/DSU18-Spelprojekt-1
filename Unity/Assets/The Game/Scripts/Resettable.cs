using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableObject
{
    public ResettableObject(Transform transform)
    {
        position = transform.localPosition;
        rotation = transform.localRotation;
        scale = transform.localScale;

        targetObject = transform;
    }

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Transform targetObject;

    public virtual void Reset()
    {
        Reposition();
    }

    public void Reposition()
    {
        targetObject.localPosition = position;
        targetObject.localRotation = rotation;
        targetObject.localScale = scale;
    }
}

public class Resettable : MonoBehaviour
{
    List<ResettableObject> storedObjects = new List<ResettableObject>();
    
    void Awake()
    {
        Camera.main.GetComponent<CameraMove>().OnLevelEnter += SaveLevel;
        Camera.main.GetComponent<LevelResetter>().AfterResetLevel += Reset;
    }

    // Update is called once per frame
    void SaveLevel(Transform room)
    {
        if (transform.parent != room)
            return;

        if(storedObjects.Count != 0)
        {
            Reset(room);
            storedObjects.Clear();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var temp = new ResettableObject(transform.GetChild(i));
            storedObjects.Add(temp);
        }
    }

    private void Reset(Transform room)
    {
        if (transform.parent != room)
            return;

        for (int i = 0; i < storedObjects.Count; i++)
        {
            storedObjects[i].Reset();
        }
    }
}