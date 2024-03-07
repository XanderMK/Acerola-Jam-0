using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionImageWaitTime, transitionImageFadeOutTime;
    [Space(10f)]
    [SerializeField] private GameObject crosshair;
    [Space(10f)]
    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup textGroup;

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

        yield return new WaitForSeconds(transitionImageWaitTime);

        transitionImageInitialColor = transitionImage.color;
        TransitionOut(transitionImageFadeOutTime);
    }

    void FixedUpdate() {
        crosshair.SetActive(playerInteraction.IsHoveringOverInteractable);
    }

    public void SetText(string newText, float fadeInTime = 0f, float stayTime = 4f, float fadeOutTime = 0f) {
        StopCoroutine("ISetText");
        textGroup.DOKill();

        text.text = newText;
        textGroup.alpha = 0f;

        StartCoroutine(ISetText(fadeInTime, stayTime, fadeOutTime));
    }

    public void SetText(string textData) {
        string[] parameters = textData.Split(":::");

        SetText(parameters[0], float.Parse(parameters[1]), float.Parse(parameters[2]), float.Parse(parameters[3]));
    }

    public void SetTextUntilFurtherNotice(string newText, float fadeInTime = 0f) {
        StopCoroutine("ISetText");

        text.text = newText;

        textGroup.DOKill();
        textGroup.DOFade(1f, fadeInTime);
    }

    public void RemoveText(float fadeOutTime) {
        StopCoroutine("ISetText");

        textGroup.DOKill();
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
}
