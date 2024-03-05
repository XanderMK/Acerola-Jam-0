using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightswitchInteractable : Interactable
{
    [SerializeField] private bool isOn;

    [SerializeField, SerializeReference] private GameObject[] toggleableObjects;
    [Space(10f)]
    [SerializeField] private Transform switchMesh;
    [SerializeField] private float onRotation, offRotation;

    private List<IToggleable> toggleables = new();

    public event UnityAction toggleOnEvent = delegate {};
    public event UnityAction toggleOffEvent = delegate {};

    private void Start() {
        foreach (GameObject toggleableObject in toggleableObjects) {
            IToggleable toggleable = toggleableObject.GetComponent<IToggleable>();
            if (toggleable == null) {
                Debug.LogError(toggleableObject.name + " is not a toggleable object!");
                continue;
            }

            toggleables.Add(toggleable);

            toggleable.Toggle(isOn);
        }
        

        SetState();
    }

    public override void Interact() {
        isOn = !isOn;

        foreach (IToggleable toggleable in toggleables)
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
