using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseOver : MonoBehaviour
{
    [SerializeField]
    UnityEvent WhileMousedOver;

    [SerializeField]
    UnityEvent OnMouseLeft;

    // Update is called once per frame
    void OnMouseOver()
    {
        WhileMousedOver?.Invoke();
    }

    void OnMouseExit()
    {
        OnMouseLeft?.Invoke();
    }
}