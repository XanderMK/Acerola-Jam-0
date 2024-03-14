using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerAnimationPart1 : MonoBehaviour
{
    [SerializeField] private Part1GameStateManager gameStateManager;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource playerVoiceSource;

    private void Awake() {
        AudioListener.volume = 0f;
    }

    public void EnablePlayerMovement() {
        gameStateManager.EnablePlayerMovement();
    }

    public void DisableCameraAnimator() {
        playerAnimator.enabled = false;
    }

    public void DisplayText(string textData) {
        string[] parameters = textData.Split(":::");

        HUD.Instance.SetText(parameters[0], float.Parse(parameters[1]), float.Parse(parameters[2]), float.Parse(parameters[3]));
    }

    public void DisplayTextUntilFurtherNotice(string textData) {
        string[] parameters = textData.Split(":::");

        HUD.Instance.SetTextUntilFurtherNotice(parameters[0], float.Parse(parameters[1]));
    }

    public void RemoveText(float fadeOutTime) {
        HUD.Instance.RemoveText(fadeOutTime);
    }

    public void SetStartLampCanBeInteractedWith() {
        gameStateManager.SetStartLampCanBeInteractedWith(true);
    }

    public void FadeTransitionImage(float time) {
        HUD.Instance.TransitionIn(time);
    }

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void PlayVoiceClip(AudioClip clip) {
        playerVoiceSource.clip = clip;
        playerVoiceSource.Play();
    }

    public void FadeAudioIn(float time) {
        DOTween.To(() => AudioListener.volume, (x) => AudioListener.volume = x, 1, time);
    }

    public void FadeAudioOut(float time) {
        DOTween.To(() => AudioListener.volume, (x) => AudioListener.volume = x, 0, time);
    }
}
