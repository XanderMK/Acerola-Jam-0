using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleInteractable : Interactable
{
    public bool isOn;

    [SerializeField, SerializeReference] private GameObject[] toggleableObjects;

    private List<IToggleable> toggleables = new();

    public UnityEvent toggleOnEvent;
    public UnityEvent toggleOffEvent;

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

        if (isOn && toggleOnEvent != null) {
            toggleOnEvent.Invoke();
        }
        else if (!isOn && toggleOffEvent != null) {
            toggleOffEvent.Invoke();
        }
    }

    public override void Interact() {
        if (!CanBeInteractedWith) return;

        Toggle();
    }

    public override void StopInteract() {}

    public void Toggle() {
        isOn = !isOn;

        foreach (IToggleable toggleable in toggleables)
            toggleable.Toggle(isOn);

        if (isOn) {
            toggleOnEvent.Invoke();
        }
        else {
            toggleOffEvent.Invoke();
        }
    }
}
