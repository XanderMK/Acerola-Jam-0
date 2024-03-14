using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    private float defaultVolume;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    void Update() {
        audioSource.volume = defaultVolume * PlayerPrefs.GetFloat("Music Volume", 1);
    }
}
