using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1GameStateManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator playerAnimator;
    [Space(10f)]
    [SerializeField] private ToggleInteractable startCutsceneLamp;

    public void EnablePlayerMovement() {
        player.enabled = true;
    }

    public void DisablePlayerMovement() {
        player.enabled = false;
    }

    public void SetStartLampCanBeInteractedWith(bool canBeInteractedWith) {
        startCutsceneLamp.SetCanBeInteractedWith(canBeInteractedWith);
    }
}
