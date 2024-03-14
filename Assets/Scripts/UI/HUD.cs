using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerInputManager input;
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionImageWaitTime, transitionImageFadeOutTime;
    [Space(10f)]
    [SerializeField] private GameObject crosshair;
    [Space(10f)]
    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup textGroup;
    [Space(10f)]
    [SerializeField] private Toggle fullscreenToggle, vsyncToggle;
    [SerializeField] private Slider musicVolumeSlider, soundVolumeSlider;
    [SerializeField] private TMP_Text musicVolumeLabel, soundVolumeLabel;

    private PlayerMovement playerMovement;
    private PlayerInteraction playerInteraction;

    private Color transitionImageInitialColor;

    public static HUD Instance;

    private List<Coroutine> currentCoroutines = new();

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start() {
        playerMovement = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerMovement>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerInteraction>();

        yield return new WaitForSeconds(transitionImageWaitTime);

        transitionImageInitialColor = transitionImage.color;
        TransitionOut(transitionImageFadeOutTime);

        input.pauseEvent += OnPause;

        ApplyGraphicsSettings();
        SetSettingsValues();
    }

    void FixedUpdate() {
        if (crosshair != null)
            crosshair.SetActive(playerInteraction.IsHoveringOverInteractable);
    }

    public void SetText(string newText, float fadeInTime = 0f, float stayTime = 4f, float fadeOutTime = 0f) {
        foreach (Coroutine coroutine in currentCoroutines) {
            StopCoroutine(coroutine);
        }
        currentCoroutines.Clear();
        textGroup.DOKill();

        text.text = newText;
        textGroup.alpha = 0f;

        currentCoroutines.Add(StartCoroutine(ISetText(fadeInTime, stayTime, fadeOutTime)));
    }

    public void SetText(string textData) {
        string[] parameters = textData.Split(":::");

        SetText(parameters[0], float.Parse(parameters[1]), float.Parse(parameters[2]), float.Parse(parameters[3]));
    }

    public void SetTextUntilFurtherNotice(string newText, float fadeInTime = 0f) {
        foreach (Coroutine coroutine in currentCoroutines) {
            StopCoroutine(coroutine);
        }
        currentCoroutines.Clear();
        textGroup.DOKill();

        text.text = newText;
        textGroup.alpha = 0f;
        
        textGroup.DOFade(1f, fadeInTime);
    }

    public void RemoveText(float fadeOutTime) {
        foreach (Coroutine coroutine in currentCoroutines) {
            StopCoroutine(coroutine);
        }
        currentCoroutines.Clear();
        textGroup.DOKill();

        textGroup.alpha = 1f;

        textGroup.DOFade(0f, fadeOutTime);
    }

    private IEnumerator ISetText(float fadeInTime, float stayTime, float fadeOutTime) {
        textGroup.DOFade(1f, fadeInTime);
        yield return new WaitForSeconds(fadeInTime + stayTime);
        textGroup.DOFade(0f, fadeOutTime);
    }

    public void TransitionOut(float time) {
        transitionImage.DOKill();
        transitionImage.DOColor(Color.clear, time);
    }

    public void TransitionIn(float time) {
        transitionImage.DOKill();
        transitionImage.DOColor(transitionImageInitialColor, time);
    }

    public void SetTextAfterTime(string data) {
        string[] values = data.Split(":::");

        StartCoroutine(ISetTextAfterTime(values[0], float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[2]), float.Parse(values[3])));
    }

    private IEnumerator ISetTextAfterTime(string text, float waitTime, float fadeInTime, float stayTime, float fadeOutTime) {
        yield return new WaitForSeconds(waitTime);

        SetText(text, fadeInTime, stayTime, fadeOutTime);
    }

    bool paused = false;
    private void OnPause() {
        if (!playerMovement.enabled && !paused) return;

        paused = !paused;

        if (paused) {
            Pause();
        }
        else {
            Unpause();
        }
    }

    private void Pause() {
        Time.timeScale = 0f;
    }

    private void Unpause() {
        Time.timeScale = 1f;
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
