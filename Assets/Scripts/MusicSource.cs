using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    public float baseVolume;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        baseVolume = audioSource.volume;
    }

    void Update() {
        audioSource.volume = baseVolume * PlayerPrefs.GetFloat("Music Volume", 1);
    }
}
