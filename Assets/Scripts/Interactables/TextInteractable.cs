using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : Interactable
{
    [SerializeField] private string textOnInteract;
    [Space(5f)]
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;
    [Space(10f)]
    [SerializeField] private AudioClip voiceClip;

    private AudioSource playerVoiceAudio;

    private void Start() {
        playerVoiceAudio = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
    }
    
    public override void Interact() {
        if (textOnInteract != string.Empty)
            HUD.Instance.SetText(textOnInteract, fadeInTime, stayTime, fadeOutTime);

        if (playerVoiceAudio != null && voiceClip != null && (!playerVoiceAudio.isPlaying || playerVoiceAudio.clip != voiceClip)) {
            playerVoiceAudio.clip = voiceClip;
            playerVoiceAudio.Play();
        }
    }

    public override void StopInteract() {}
}
