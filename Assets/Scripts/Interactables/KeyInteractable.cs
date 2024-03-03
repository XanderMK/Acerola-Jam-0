using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] private DoorInteractable doorToUnlock;
    [SerializeField] private string textOnPickup;
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;

    public override void Interact() {
        doorToUnlock.SetLocked(false);
        HUD.Instance.SetText(textOnPickup, fadeInTime, stayTime, fadeOutTime);
        Destroy(gameObject);
    }

    public override void StopInteract() {

    }
}
