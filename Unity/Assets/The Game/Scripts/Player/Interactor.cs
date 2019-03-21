using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    KeyCode button = KeyCode.E;

    Interactable currentInteractable;
    Jump jump;

    private void Awake()
    {
        OmniDisabler.SetActiveBasedOnEnable(this);
        jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(button) && jump.IsGrounded)
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