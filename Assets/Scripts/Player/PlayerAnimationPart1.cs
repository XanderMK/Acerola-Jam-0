using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationPart1 : MonoBehaviour
{
    [SerializeField] private Part1GameStateManager gameStateManager;
    
    public void LookAtLamp() {
        gameStateManager.PlayerLookAtLampSequence();
    }

    public void EnablePlayerMovement() {
        gameStateManager.EnablePlayerMovement();
    }
}
