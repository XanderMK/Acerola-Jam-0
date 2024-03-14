using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2_5LockAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] chains;
    [SerializeField] private ParticleSystem[] chainBreakSystems;
    [SerializeField] private PlayerMovement playerMovement;

    public void BreakRemainingChains() {
        foreach (GameObject chain in chains) {
            chain.SetActive(false);
        }

        foreach (ParticleSystem chainBreakSystem in chainBreakSystems) {
            if (chainBreakSystem != null && !chainBreakSystem.isPlaying) {
                chainBreakSystem.Play();
            }
        }
    }

    public void DisablePlayerMovement() {
        playerMovement.enabled = false;
    }

    public void EnablePlayerMovement() {
        playerMovement.enabled = true;
    }
}
