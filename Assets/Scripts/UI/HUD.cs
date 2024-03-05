using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionImageFadeInTime, transitionImageFadeOutTime;
    [Space(10f)]
    [SerializeField] private GameObject crosshair;
    [Space(10f)]
    [SerializeField] private TMP_Text text;

    private PlayerInteraction playerInteraction;

    private Color transitionImageInitialColor;

    public static HUD Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    IEnumerator Start() {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

        yield return new WaitForSeconds(1f);

        transitionImageInitialColor = transitionImage.color;
        transitionImage.DOColor(Color.clear, transitionImageFadeOutTime);
    }

    void FixedUpdate() {
        crosshair.SetActive(playerInteraction.IsHoveringOverInteractable);
    }

    public void SetText(string newText, float fadeInTime = 0f, float stayTime = 4f, float fadeOutTime = 0f) {
        StopAllCoroutines();
        text.DOKill();

        text.text = newText;
        text.color = Color.clear;

        StartCoroutine(ISetText(fadeInTime, stayTime, fadeOutTime));
    }

    public void SetTextUntilFurtherNotice(string newText, float fadeInTime = 0f) {
        StopAllCoroutines();

        text.text = newText;

        text.DOKill();
        text.DOColor(Color.white, fadeInTime);
    }

    public void RemoveText(float fadeOutTime) {
        StopAllCoroutines();

        text.DOKill();
        text.DOColor(Color.clear, fadeOutTime);
    }

    private IEnumerator ISetText(float fadeInTime, float stayTime, float fadeOutTime) {
        text.DOColor(Color.white, fadeInTime);
        yield return new WaitForSeconds(fadeInTime + stayTime);
        text.DOColor(Color.clear, fadeOutTime);
    }

    public void TransitionIn() {
        transitionImage.DOKill();
        transitionImage.DOColor(Color.clear, transitionImageFadeOutTime);
    }

    public void TransitionOut() {
        transitionImage.DOKill();
        transitionImage.DOColor(transitionImageInitialColor, transitionImageFadeOutTime);
    }
}
