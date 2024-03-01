using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    [SerializeField] private float closedRotation, maxRotation;
    [SerializeField] private float currentRotation;
    [Space(10f)]
    [SerializeField] private float moveForce;
    
    private Vector3 currentLocalInteractionPoint;
    private float currentInteractionDistance;

    private Rigidbody rb;
    private Transform playerCam;

    private bool isBeingInteracted = false;
    private bool isOpen = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main.transform;

        isOpen = !Mathf.Approximately(currentRotation, closedRotation);
    }

    private void FixedUpdate() {
        if (transform.localEulerAngles.y > closedRotation && !isBeingInteracted) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, closedRotation, transform.localEulerAngles.z);
        }
        else if (transform.localEulerAngles.y < maxRotation) {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, maxRotation, transform.localEulerAngles.z);
            rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y * -0.333f, rb.angularVelocity.z);
        }

    }

    public override void Interact()
    {
        RaycastHit hit;
        Physics.Raycast(playerCam.position, playerCam.forward, out hit);

        currentLocalInteractionPoint = transform.InverseTransformPoint(hit.point);
        currentInteractionDistance = Vector3.Distance(hit.point, playerCam.position);

        isBeingInteracted = true;

        StartCoroutine(MoveDoor());
    }

    public override void StopInteract()
    {
        StopAllCoroutines();
        isBeingInteracted = false;
    }

    private IEnumerator MoveDoor() {
        while (true) {
            if (rb.isKinematic) {
                rb.isKinematic = false;
            }

            Vector3 targetPoint = playerCam.position + playerCam.forward * currentInteractionDistance;

            Vector3 worldSpaceInteractionPoint = transform.TransformPoint(currentLocalInteractionPoint);

            rb.AddForceAtPosition(moveForce * (targetPoint - worldSpaceInteractionPoint), worldSpaceInteractionPoint);

            yield return new WaitForFixedUpdate();
        }
    }
}
