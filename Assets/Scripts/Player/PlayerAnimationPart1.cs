using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationPart1 : MonoBehaviour
{
    [SerializeField] private Part1GameStateManager gameStateManager;
    [SerializeField] private Animator playerAnimator;

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
        HUD.Instance.RemoveText(time);

        HUD.Instance.TransitionIn(time);
    }

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
