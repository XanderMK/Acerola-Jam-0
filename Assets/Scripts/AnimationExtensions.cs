using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class AnimationExtensions : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Part4GameManager part4GameManager;

    private new Animation animation;

    private void Awake() {
        animation = GetComponent<Animation>();
    }

    public void Play(string anim) {
        animation.Play(anim);
    }

    public void DisplayText(string text) {
        HUD.Instance.SetText(text);
    }

    public void PlayMusicClip(AudioClip clip) {
        musicSource.PlayOneShot(clip);
    }

    public void PlayAudioClip(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void TransitionOut(float time) {
        HUD.Instance.TransitionOut(time);
    }

    public void TransitionIn(float time) {
        HUD.Instance.TransitionIn(time);
    }

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void EnablePlayerMovement() {
        playerMovement.enabled = true;
    }

    public void DisablePlayerMovement() {
        playerMovement.enabled = false;
        playerRb.velocity = Vector3.zero;
    }

    public void FadeAbarritionColor(float time) {
        part4GameManager.FadeAbarritionColor(time);
    }
}
