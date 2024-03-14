using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part3StartText : MonoBehaviour
{
    [SerializeField] private string textToDisplay;
    [SerializeField] private float waitTime, fadeInTime, stayTime, fadeOutTime;
    [SerializeField] private AudioClip voiceClip;

    [SerializeField] private GameObject abarrition;

    private AudioSource voiceSource;

    private IEnumerator Start() {
        voiceSource = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

        yield return new WaitForSeconds(waitTime);

        voiceSource.clip = voiceClip;
        voiceSource.Play();

        HUD.Instance.SetText(textToDisplay, fadeInTime, stayTime, fadeOutTime);
        
        yield return new WaitForSeconds(1f);

        abarrition.SetActive(true);
    }
}
