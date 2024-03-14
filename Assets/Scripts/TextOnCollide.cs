using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnCollide : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private float fadeInTime, stayTime, fadeOutTime;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;


    private void OnTriggerEnter(Collider col) {
        HUD.Instance.SetText(text, fadeInTime, stayTime, fadeOutTime);
        source?.PlayOneShot(clip);

        Destroy(gameObject);
    }
}
