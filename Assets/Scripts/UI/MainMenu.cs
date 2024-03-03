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
    [SerializeField] private float startYPosition, startTransitionTime;
    [SerializeField] private float optionsYPosition, optionsTransitionTime;
    [Space(10f)]
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionImageFadeOutTime, transitionImageFadeInTime;
    private Color transitionImageInitialColor;
    private bool isTransitioning = false;


    private void Start() {
        transitionImageInitialColor = transitionImage.color;
        transitionImage.DOColor(Color.clear, transitionImageFadeOutTime);
    }

    public void OnStart() {
        if (isTransitioning) return;

        isTransitioning = true;
        camera.DOMoveY(startYPosition, startTransitionTime).SetEase(Ease.InCubic).OnComplete( () => { 
            SceneManager.LoadScene(1);
        } );
    }

    public void OnOptions() {
        if (isTransitioning) return;

        isTransitioning = true;
        camera.DOMoveY(optionsYPosition, optionsTransitionTime).SetEase(Ease.InCubic).OnComplete( () => { 
            isTransitioning = false; 
        } );
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
}
