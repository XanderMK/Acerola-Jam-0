using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Inspector Values
    [SerializeField] private PlayerInputManager input;
    [Space(20f)]
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float acceleration;
    [Space(10f)]
    [SerializeField] private Transform groundCastPoint;
    [SerializeField] private float maxGroundCastDistance;
    [SerializeField] private LayerMask groundCastMask;
    [SerializeField] private float maxGroundAngle;
    [Space(20f)]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float cameraWalkingFOV, cameraRunningFOV;
    [SerializeField] private float cameraFOVChangeTime;

    // Local Components
    private Rigidbody rb;
    private CapsuleCollider col;
    private Transform cameraTransform;
    private new Camera camera;

    // Local Variables
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float currentMoveSpeed;
    [SerializeField] private float lookRotationX, lookRotationY;

    private void OnEnable() {
        SetInputCallbacks();

        moveInput = Vector2.zero;
        lookInput = Vector2.zero;
    }

    private void OnDisable() {
        RemoveInputCallbacks();

        camera.DOKill();
        camera.DOFieldOfView(cameraWalkingFOV, cameraFOVChangeTime).SetEase(Ease.OutCubic);
    }


    private void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<CapsuleCollider>();
        cameraTransform = Camera.main.transform.parent;
        camera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;

        currentMoveSpeed = walkSpeed;
        lookRotationY = transform.localEulerAngles.y;
    }

    Vector3 currentAccel;
    private void FixedUpdate() {
        float rbVelocityY = rb.velocity.y;

        Vector3 moveDirection = transform.forward * moveInput.y * currentMoveSpeed +
                                transform.right * moveInput.x * currentMoveSpeed;

        GroundData groundData = GetGroundData();

        // Turn gravity off if already on the ground
        if (groundData.hitGround)
            rb.useGravity = false;
        else
            rb.useGravity = true;

        // Move along the ground
        Vector3 groundNormal = groundData.hitInfo.normal;
        if (Mathf.Abs(Vector3.Dot(Vector3.up, groundNormal)) * 90f < maxGroundAngle)  {
            groundNormal = Vector3.up;
            rb.AddForce(Vector3.down * 25f, ForceMode.Acceleration);
        }

        moveDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal);
        if (rbVelocityY < 0f)
            moveDirection.y += rbVelocityY;

        rb.AddForce((moveDirection - rb.velocity) * acceleration, ForceMode.Acceleration);
    }

    private void Update() {
        lookRotationY += lookInput.x * mouseSensitivity * Time.deltaTime;
        lookRotationX -= lookInput.y * mouseSensitivity * Time.deltaTime;

        lookRotationX = Mathf.Clamp(lookRotationX, -80f, 80f);

        transform.localEulerAngles = Vector3.up * lookRotationY;
        cameraTransform.localEulerAngles = Vector3.right * lookRotationX;
    }

    private struct GroundData {
        public GroundData(bool hitGround, RaycastHit hitInfo) {
            this.hitGround = hitGround;
            this.hitInfo = hitInfo;
        }
        
        public bool hitGround;
        public RaycastHit hitInfo;
    };

    private GroundData GetGroundData() {
        bool hasHit;
        RaycastHit hit;

        // Cast a sphere slightly smaller than the player to get ground data
        hasHit = Physics.SphereCast(groundCastPoint.position, col.radius - 0.01f, Vector3.down, out hit, maxGroundCastDistance);

        return new GroundData(hasHit, hit);
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

        camera.DOKill();
        camera.DOFieldOfView(cameraRunningFOV, cameraFOVChangeTime).SetEase(Ease.OutCubic);
    }

    private void StopRunSubscriber() {
        currentMoveSpeed = walkSpeed;

        camera.DOKill();
        camera.DOFieldOfView(cameraWalkingFOV, cameraFOVChangeTime).SetEase(Ease.OutCubic);
    }
}
