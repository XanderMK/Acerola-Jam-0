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
    private GameObject currentlyHeldItem;
    private Interactable currentlyInteractable;
    private Interactable currentlyInteractedObject;

    public bool IsHoveringOverInteractable {
        get {
            if (currentlyInteractable != null) {
                return (currentlyInteractable.CanBeInteractedWith);
            }
            return false;
        }
    }

    private Rigidbody rb;

    private void OnEnable() {
        SetInputCallbacks();
    }

    private void OnDisable() {
        RemoveInputCallbacks();
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = Vector3.zero;
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
        if (currentlyInteractable != null) {
            currentlyInteractable.Interact();
            currentlyInteractedObject = currentlyInteractable;
        }
    }

    private void StopInteractSubscriber() {
        if (currentlyInteractedObject != null) {
            currentlyInteractedObject.StopInteract();

            currentlyInteractedObject = null;
        }
    }

    private Interactable GetCurrentlyInteractable() {
        RaycastHit hit;
        if (Physics.Raycast(interactableCastTransform.position, interactableCastTransform.forward, out hit, maxInteractableDistance, interactionMask)) {
            Interactable interactable = hit.transform.GetComponentInParent<Interactable>();

            return interactable;
        }
        return null;
    }
}
