using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Inspector Values
    [SerializeField] private PlayerInputManager input;
    [Space(10f)]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [Space(10f)]
    [SerializeField] private float mouseSensitivity;

    // Local Components
    private Rigidbody rb;
    private new Transform camera;

    // Local Variables
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float currentMoveSpeed;
    private float lookRotationX, lookRotationY;

    private void OnEnable() {
        SetInputCallbacks();
    }

    private void OnDisable() {
        RemoveInputCallbacks();
    }


    private void Start() {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;

        currentMoveSpeed = walkSpeed;
    }

    private void FixedUpdate() {
        rb.velocity = transform.forward * moveInput.y * currentMoveSpeed +
                      transform.right * moveInput.x * currentMoveSpeed;
    }

    private void Update() {
        lookRotationY += lookInput.x * mouseSensitivity * Time.deltaTime;
        lookRotationX -= lookInput.y * mouseSensitivity * Time.deltaTime;

        lookRotationX = Mathf.Clamp(lookRotationX, -80f, 80f);

        transform.localEulerAngles = Vector3.up * lookRotationY;
        camera.localEulerAngles = Vector3.right * lookRotationX;
    }


    private void SetInputCallbacks() {
        input.moveEvent += MoveSubscriber;
        input.lookEvent += LookSubscriber;
        input.startRunEvent += StartRunSubscriber;
        input.stopRunEvent += StopRunSubscriber;
    }

    private void RemoveInputCallbacks() {
        input.moveEvent -= MoveSubscriber;
        input.lookEvent -= LookSubscriber;
        input.startRunEvent -= StartRunSubscriber;
        input.stopRunEvent -= StopRunSubscriber;
    }

    private void MoveSubscriber(Vector2 value) {
        moveInput = value;
    }

    private void LookSubscriber(Vector2 value) {
        lookInput = value;
    }

    private void StartRunSubscriber() {
        currentMoveSpeed = runSpeed;
    }

    private void StopRunSubscriber() {
        currentMoveSpeed = walkSpeed;
    }
}
