using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{

    public UnityEvent interactEvent;
    public UnityEvent stopInteractEvent;

    public override void Interact()
    {
        interactEvent?.Invoke();
    }

    public override void StopInteract() 
    {
        stopInteractEvent?.Invoke();
    }
}
