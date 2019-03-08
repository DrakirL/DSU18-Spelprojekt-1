using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    KeyCode button = KeyCode.E;

    public Interactable currentInteractable;

    private void Awake()
    {
        OmniDisabler.SetActiveBasedOnEnable(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(button) && OmniDisabler.IsEnabled)
        {
            currentInteractable.Interact(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.transform.GetComponent<Interactable>();
        if (interactable != null)
            currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.transform.GetComponent<Interactable>();
        if (interactable == null)
            return;

        if (interactable == currentInteractable)
            currentInteractable = null;
    }
}