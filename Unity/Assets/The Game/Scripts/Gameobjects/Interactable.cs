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
    UnityEventGO OnInteractWith;


    [SerializeField]
    UnityEvent OnInteract;



    public void Interact(GameObject obj)
    {
        
        OnInteractWith.Invoke(obj);
        OnInteract.Invoke();
    }
}
