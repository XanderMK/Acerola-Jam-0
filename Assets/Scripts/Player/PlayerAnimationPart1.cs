using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationPart1 : MonoBehaviour
{
    [SerializeField] private Part1GameStateManager gameStateManager;
    [SerializeField] private Animator playerAnimator;
    
    public void LookAtLamp() {
        gameStateManager.PlayerLookAtLampSequence();
    }

    public void EnablePlayerMovement() {
        gameStateManager.EnablePlayerMovement();
    }

    public void DisableCameraAnimator() {
        playerAnimator.enabled = false;
    }
}
