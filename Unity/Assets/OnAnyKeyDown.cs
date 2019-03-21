using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnyKeyDown : MonoBehaviour
{

    [SerializeField]
    UnityEvent OnKeyDown;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            OnKeyDown.Invoke();
    }
}
