using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    string button;

    public Interactable currentInteractable;

    // Update is called once per frame
    void Update()
    {
        if (currentInteractable != null && Input.GetAxisRaw(button) == 1)
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
