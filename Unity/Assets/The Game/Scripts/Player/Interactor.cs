using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    KeyCode button = KeyCode.E;

    public Interactable currentInteractable;

    bool isEnabled = true;
    private void Awake()
    {
        var death = GetComponent<Player_Death>();

        death.BeforeDie += Disable;
        death.AfterDie += Reenable;
    }

    void Disable(CauseOfDeath c)
    {
        isEnabled = false;
    }

    void Reenable()
    {
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(button) && Time.timeScale != 0 && isEnabled)
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