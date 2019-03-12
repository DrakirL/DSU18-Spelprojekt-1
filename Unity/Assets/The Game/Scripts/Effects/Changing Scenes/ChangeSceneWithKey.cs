using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneWithKey : ChangeScene
{
    [SerializeField]
    KeyCode key = KeyCode.Escape;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
            Change();
    }
}
