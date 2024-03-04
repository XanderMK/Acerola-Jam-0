using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [SerializeField] private bool isOn = false;
    [Space(10f)]
    [SerializeField] private GameObject light;
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
        if (!isOn || Mathf.Abs(rb.angularVelocity.y) > maxAngularVelocity) return;

        rb.AddTorque(Vector3.up * spinAcceleration * (spinDirection ? 1 : -1));
    }

    private void SetFanValues() {
        light.SetActive(isOn);

        mesh.material = (isOn ? lightOn : lightOff);
    }

    public void SetFanOn(bool on) {
        isOn = on;

        SetFanValues();
    }
}
