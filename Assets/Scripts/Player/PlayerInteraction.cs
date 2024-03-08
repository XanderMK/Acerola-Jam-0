using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInputManager input;
    [Space(20f)]
    [Header("Detection")]
    [SerializeField] private Transform interactableCastTransform;
    [SerializeField] private float maxInteractableDistance;
    [SerializeField] private LayerMask interactionMask;
    [Header("Interaction Handling")]
    private Interactable[] currentlyInteractable;
    private List<Interactable> currentlyInteractedObjects = new();

    public bool IsHoveringOverInteractable {
        get {
            foreach (Interactable interactable in currentlyInteractable) {
                if (interactable.CanBeInteractedWith)
                    return true;
            }
            
            return false;
        }
    }

    private void OnEnable() {
        SetInputCallbacks();
        currentlyInteractable = new Interactable[0];
    }

    private void OnDisable() {
        RemoveInputCallbacks();
    }

    private void FixedUpdate() {
        currentlyInteractable = GetCurrentlyInteractable();
    }

    private void SetInputCallbacks() {
        input.interactEvent += InteractSubscriber;
        input.stopInteractEvent += StopInteractSubscriber;
    }

    private void RemoveInputCallbacks() {
        input.interactEvent -= InteractSubscriber;
        input.stopInteractEvent -= StopInteractSubscriber;
    }

    private void InteractSubscriber() {
        foreach (Interactable interactable in currentlyInteractable) {
            interactable.Interact();
            currentlyInteractedObjects.Add(interactable);
        }
        
    }

    private void StopInteractSubscriber() {
        foreach (Interactable interactable in currentlyInteractedObjects) {
            interactable.StopInteract();
        }
        currentlyInteractedObjects.Clear();
    }

    private Interactable[] GetCurrentlyInteractable() {
        RaycastHit hit;
        if (Physics.Raycast(interactableCastTransform.position, interactableCastTransform.forward, out hit, maxInteractableDistance, interactionMask)) {
            Interactable[] interactables = hit.transform.GetComponentsInParent<Interactable>();

            return interactables;
        }
        return new Interactable[0];
    }
}
