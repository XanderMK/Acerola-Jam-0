using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnopenableDoorInteractable : Interactable
{
    [SerializeField] private string textOnTryOpen;
    [SerializeField] private bool playAnimation = true;
    [Space(5f)]
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;
    private Animation anim;

    private void Start() {
        anim = GetComponent<Animation>();
    }
    
    public override void Interact() {
        if (playAnimation)
            anim.Play();
        if (textOnTryOpen != string.Empty)
            HUD.Instance.SetText(textOnTryOpen, fadeInTime, stayTime, fadeOutTime);
    }

    public override void StopInteract() {}
}
