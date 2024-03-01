using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : Interactable
{
    [SerializeField] private float moveForce;
    
    private Vector3 currentLocalInteractionPoint;
    private float currentInteractionDistance;

    private Rigidbody rb;
    private HingeJoint hingeJoint;
    private Transform playerCam;

    public event UnityAction openEvent = delegate {};
    public event UnityAction closeEvent = delegate {};
    public event UnityAction moveEvent = delegate {};

    bool isBeingInteractedWith = false;
    bool openDirection;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hingeJoint = GetComponent<HingeJoint>();
        playerCam = Camera.main.transform;

        openDirection = hingeJoint.limits.min < 0f; // true is counterclockwise, false is clockwise
    }

    private void FixedUpdate() {
        if (((hingeJoint.angle >= -0.05f && openDirection) || (hingeJoint.angle <= 0.05f && !openDirection)) && !isBeingInteractedWith) {
            rb.isKinematic = true;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
        }
        else {
            rb.isKinematic = false;
        }
    }

    public override void Interact()
    {
        RaycastHit hit;
        Physics.Raycast(playerCam.position, playerCam.forward, out hit);

        currentLocalInteractionPoint = transform.InverseTransformPoint(hit.point);
        currentInteractionDistance = Vector3.Distance(hit.point, playerCam.position);

        isBeingInteractedWith = true;

        StartCoroutine(MoveDoor());
    }

    public override void StopInteract()
    {
        StopAllCoroutines();

        isBeingInteractedWith = false;
    }

    private IEnumerator MoveDoor() {
        while (true) {
            Vector3 targetPoint = playerCam.position + playerCam.forward * currentInteractionDistance;

            Vector3 worldSpaceInteractionPoint = transform.TransformPoint(currentLocalInteractionPoint);

            rb.AddForceAtPosition(moveForce * (targetPoint - worldSpaceInteractionPoint), worldSpaceInteractionPoint);

            yield return new WaitForFixedUpdate();
        }
    }
}
