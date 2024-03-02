using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    private PlayerInteraction playerInteraction;

    void Start() {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    void FixedUpdate() {
        crosshair.gameObject.SetActive(playerInteraction.IsHoveringOverInteractable);
    }
}
