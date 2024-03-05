using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour, IToggleable
{
    [SerializeField] private bool isOn = false;
    [Space(10f)]
    [SerializeField] private new GameObject light;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material lightOn, lightOff;
    [Space(10f)]
    [SerializeField] private bool spinDirection;
    [SerializeField] private float spinAcceleration;
    [SerializeField] private float maxAngularVelocity;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponentInChildren<Rigidbody>();

        SetFanValues();
    }

    private void FixedUpdate() {
        if (!isOn || rb == null || Mathf.Abs(rb.angularVelocity.y) > maxAngularVelocity) return;

        rb.AddTorque(Vector3.up * spinAcceleration * (spinDirection ? 1 : -1));
    }

    private void SetFanValues() {
        light.SetActive(isOn);

        mesh.material = (isOn ? lightOn : lightOff);
    }

    public void Toggle(bool state) {
        isOn = state;

        SetFanValues();
    }
}
