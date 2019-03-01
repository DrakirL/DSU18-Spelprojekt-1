using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent OnInteract;

    [SerializeField]
    protected UnityEvent OnFirstInteract;
    bool hasInteractedBefore;

    private void Awake()
    {
        var reset = Camera.main.GetComponent<LevelResetter>();
        reset.BeforeLevelReset += () => hasInteractedBefore = false;
    }

    public virtual void Interact(GameObject obj)
    {
        OnInteract.Invoke();

        if (!hasInteractedBefore)
            OnFirstInteract.Invoke();
    }
}