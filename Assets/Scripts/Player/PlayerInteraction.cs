using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInputManager input;
    [Space(20f)]
    [Header("Detection")]
    [SerializeField] private Transform interactableCastTransform;
    [SerializeField] private float maxInteractableDistance;
    [Header("Interaction Handling")]
    private GameObject currentlyHeldItem;
    private Interactable currentlyInteractedObject;

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

    private void SetInputCallbacks() {
        input.interactEvent += InteractSubscriber;
        input.stopInteractEvent += StopInteractSubscriber;
    }

    private void RemoveInputCallbacks() {
        input.interactEvent -= InteractSubscriber;
        input.stopInteractEvent -= StopInteractSubscriber;
    }

    private void InteractSubscriber() {
        RaycastHit hit;
        if (Physics.Raycast(interactableCastTransform.position, interactableCastTransform.forward, out hit, maxInteractableDistance)) {
            Interactable interactable = hit.transform.GetComponentInParent<Interactable>();
            if (interactable != null) {
                interactable.Interact();
                currentlyInteractedObject = interactable;
            }
        }
        Debug.DrawLine(interactableCastTransform.position, interactableCastTransform.position + interactableCastTransform.forward * maxInteractableDistance);
    }

    private void StopInteractSubscriber() {
        if (currentlyInteractedObject != null) {
            currentlyInteractedObject.StopInteract();

            currentlyInteractedObject = null;
        }
    }
}
