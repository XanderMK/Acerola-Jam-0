using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [SerializeField] private Music menuMusic;
    [SerializeField] private float startYPosition, startTransitionTime;
    [SerializeField] private float optionsYPosition, optionsXRotation, optionsTransitionTime;
    [Space(10f)]
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionImageFadeOutTime, transitionImageFadeInTime;
    [Space(10f)]
    [SerializeField] private Toggle fullscreenToggle, vsyncToggle;
    [SerializeField] private Slider musicVolumeSlider, soundVolumeSlider;
    [SerializeField] private TMP_Text musicVolumeLabel, soundVolumeLabel;
    private Color transitionImageInitialColor;
    private Vector3 initialPosition, initialRotation;
    private bool isTransitioning = false;


    private void Start() {
        transitionImageInitialColor = transitionImage.color;
        transitionImage.DOColor(Color.clear, transitionImageFadeOutTime);

        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;

        ApplyGraphicsSettings();
        SetSettingsValues();
    }

    public void OnStart() {
        if (isTransitioning) return;

        isTransitioning = true;
        camera.DOMoveY(startYPosition, startTransitionTime).SetEase(Ease.InCubic).OnComplete( () => { 
            SceneManager.LoadScene(2);
        } );
        transitionImage.DOColor(transitionImageInitialColor, startTransitionTime-1f).SetEase(Ease.InCubic);
        DOTween.To(() => menuMusic.baseVolume, (x) => menuMusic.baseVolume = x, 0f, startTransitionTime);
    }

    public void OnOptions() {
        if (isTransitioning) return;

        isTransitioning = true;
        camera.DOMoveY(optionsYPosition, optionsTransitionTime).SetEase(Ease.InOutSine).OnComplete( () => { 
            isTransitioning = false; 
        } );
        camera.DORotate(new Vector3(optionsXRotation, 0f, 0f), optionsTransitionTime).SetEase(Ease.InOutSine);
    }

    public void OnQuit() {
        if (isTransitioning) return;

        isTransitioning = true;
        transitionImage.DOKill();
        transitionImage.DOColor(transitionImageInitialColor, transitionImageFadeInTime).OnComplete(
            () => {
                Application.Quit();
            }
        );
    }

    public void OnBack() {
        if (isTransitioning) return;

        isTransitioning = true;
        camera.DOMoveY(initialPosition.y, optionsTransitionTime).SetEase(Ease.InOutSine);
        camera.DORotate(initialRotation, optionsTransitionTime).SetEase(Ease.InOutSine).OnComplete(
            () => {
                isTransitioning = false;
            }
        );
    }

    

    private void SetSettingsValues() {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        vsyncToggle.isOn = PlayerPrefs.GetInt("VSync") == 1 ? true : false;
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", 1);
        soundVolumeSlider.value = PlayerPrefs.GetFloat("Sound Volume", 1);
    }

    public void ChangeFullscreen() {
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
    }

    public void ChangeVSync() {
        PlayerPrefs.SetInt("VSync", vsyncToggle.isOn ? 1 : 0);
    }

    public void ChangeMusicVolume() {
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
        musicVolumeLabel.text = "Music Volume: " + Mathf.Round(musicVolumeSlider.value * 100) + "%";
    }

    public void ChangeSoundVolume() {
        PlayerPrefs.SetFloat("Sound Volume", soundVolumeSlider.value);
        soundVolumeLabel.text = "Sound Volume: " + Mathf.Round(soundVolumeSlider.value * 100) + "%";
    }

    public void ApplyGraphicsSettings() {
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1 ? true : false;
        bool vsync = PlayerPrefs.GetInt("VSync", 1) == 1 ? true : false;

        Screen.fullScreen = fullscreen;
        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }
}
