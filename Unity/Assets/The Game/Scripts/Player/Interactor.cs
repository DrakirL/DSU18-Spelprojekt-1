using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    string button;

    public Interactable currentInteractable;

    bool isEnabled = true;
    private void Awake()
    {
        var death = GetComponent<Player_Death>();
        death.BeforeDie += Disable;
        death.AfterDie += Reenable;
    }

    void Disable()
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
        if (currentInteractable != null && Input.GetAxisRaw(button) == 1 && Time.timeScale != 0 && isEnabled)
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
}