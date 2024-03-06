using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1GameStateManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator playerCamAnimator;
    [Space(10f)]
    [SerializeField] private LightswitchInteractable startCutsceneLightswitch;
    [SerializeField] private AudioSource bangingSource1, bangingSource2, bangingSource3;
    [SerializeField] private DoorInteractable bangingDoor1, bangingDoor2, bangingDoor3;
    [SerializeField] private GameObject abarrition, endCutsceneTrigger;

    private void Start() {
        startCutsceneLightswitch.toggleOnEvent.AddListener(OnStartCutsceneLightSwitch);
    }

    private void OnStartCutsceneLightSwitch() {
        startCutsceneLightswitch.toggleOnEvent.AddListener(OnStartCutsceneLightSwitch);

        HUD.Instance.RemoveText(0.2f);
        playerCamAnimator.Play("Game Intro After Lamp");
    }

    public void PlayerLookAtLampSequence() {
        HUD.Instance.SetTextUntilFurtherNotice("Press \"E\" to interact", 3);
    }

    public void EnablePlayerMovement() {
        player.enabled = true;
    }

    public void DisablePlayerMovement() {
        player.enabled = false;
    }
}
