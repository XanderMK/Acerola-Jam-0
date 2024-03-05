using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightswitchInteractable : Interactable
{
    [SerializeField] private bool isOn;

    [SerializeField, SerializeReference] private GameObject toggleableObject;
    [Space(10f)]
    [SerializeField] private Transform switchMesh;
    [SerializeField] private float onRotation, offRotation;

    private IToggleable toggleable;

    public event UnityAction toggleOnEvent = delegate {};
    public event UnityAction toggleOffEvent = delegate {};

    private void Start() {
        toggleable = toggleableObject.GetComponent<IToggleable>();
        if (toggleable == null) {
            Debug.LogError(toggleableObject.name + " is not a toggleable object!");
        }

        SetState();
    }

    public override void Interact() {
        isOn = !isOn;

        toggleable.Toggle(isOn);

        if (isOn) {
            toggleOnEvent.Invoke();
        }
        else {
            toggleOffEvent.Invoke();
        }

        SetState();
    }

    public override void StopInteract() {}

    private void SetState() {
        switchMesh.localEulerAngles = new Vector3(switchMesh.localEulerAngles.x, isOn ? onRotation : offRotation, switchMesh.localEulerAngles.z);
    }
}
