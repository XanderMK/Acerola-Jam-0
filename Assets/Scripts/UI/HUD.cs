using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [Space(10f)]
    [SerializeField] private TMP_Text text;

    private PlayerInteraction playerInteraction;

    public static HUD Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        Debug.Log(playerInteraction);
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

    private IEnumerator ISetText(float fadeInTime, float stayTime, float fadeOutTime) {
        text.DOColor(Color.white, fadeInTime);
        yield return new WaitForSeconds(fadeInTime + stayTime);
        text.DOColor(Color.clear, fadeOutTime);
    }
}
