using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool CanBeInteractedWith = true;
    public abstract void Interact();
    public abstract void StopInteract();
    public void SetCanBeInteractedWith (bool value) {
        CanBeInteractedWith = value;
    }
}
