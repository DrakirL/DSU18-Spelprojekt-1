using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventGO : UnityEvent<GameObject>
{

}

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected UnityEventGO OnInteractWith;

    [SerializeField]
    protected UnityEvent OnInteract;

    public virtual void Interact(GameObject obj)
    {
        OnInteractWith.Invoke(obj);
        OnInteract.Invoke();
    }
}
