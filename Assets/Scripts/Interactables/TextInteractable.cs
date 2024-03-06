using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : Interactable
{
    [SerializeField] private string textOnInteract;
    [Space(5f)]
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;
    
    public override void Interact() {
        if (textOnInteract != string.Empty)
            HUD.Instance.SetText(textOnInteract, fadeInTime, stayTime, fadeOutTime);
    }

    public override void StopInteract() {}
}
