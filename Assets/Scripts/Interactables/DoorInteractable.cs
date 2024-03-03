using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : Interactable
{
    [SerializeField] private Transform door;
    [Space(10f)]
    [SerializeField] private float closedRotation, openRotation;
    [SerializeField] private bool isOpen;
    [Space(10f)]
    [SerializeField] private float doorMoveTime;
    [Space(10f)]
    [SerializeField] private bool isLocked;
    [SerializeField] private string textOnOpenWhenLocked;
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;

    public event UnityAction openEvent = delegate {};
    public event UnityAction closeEvent = delegate {};

    private Rigidbody rb;
    private Animation anim;
    private bool playerInside = false;

    private void Start() {
        rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponent<Animation>();
    }

    public override void Interact() {
        if (playerInside) return;

        if (isLocked) {
            HUD.Instance.SetText(textOnOpenWhenLocked, fadeInTime, stayTime, fadeOutTime);
            anim.Play();
            return;
        }

        isOpen = !isOpen;

        StopAllCoroutines();
        StartCoroutine(RotateTo((isOpen ? openRotation : closedRotation)));
        currentVelocity = 0f;
    }

    public override void StopInteract() {}

    public void SetLocked(bool value) {
        isLocked = value;
    }

    float currentVelocity;
    private IEnumerator RotateTo(float yRot) {
        while ((int)Mathf.Abs(yRot - door.transform.localEulerAngles.y) % 360 > 0.15f) {
            Vector3 newRotation = Vector3.up * Mathf.SmoothDampAngle(door.localEulerAngles.y, yRot, ref currentVelocity, doorMoveTime);

            door.localEulerAngles = newRotation;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.transform.root.CompareTag("Player")) {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.transform.root.CompareTag("Player")) {
            playerInside = false;
        }
    }
}
