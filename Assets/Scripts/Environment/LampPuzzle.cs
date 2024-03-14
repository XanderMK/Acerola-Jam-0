using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPuzzle : MonoBehaviour
{
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioClip interactionClip;

    [SerializeField] private ToggleInteractable lamp1, lamp2, lamp3, lamp4;

    [SerializeField] private Part2_5GameManager gameManager;

    int timesInteracted = 0;
    bool completed = false;
    
    public void Interact() {
        if (completed) return;

        timesInteracted++;

        if (timesInteracted == 2) {
            HUD.Instance.SetText("These lamps seem odd...", 0.3f, 3, 0.2f);
            voiceSource.PlayOneShot(interactionClip);
        }

        if (lamp1.isOn && lamp2.isOn && lamp3.isOn && lamp4.isOn) {
            gameManager.BreakChain();
            StopAllCoroutines();
            completed = true;
        }
    }

    public void TurnLampOffAfterTime(string data) {
        string[] values = data.Split(":::");

        ToggleInteractable lamp = null;

        switch (int.Parse(values[0])) {
            case 1:
                lamp = lamp1;
                break;
            case 2:
                lamp = lamp2;
                break;
            case 3:
                lamp = lamp3;
                break;
            case 4:
                lamp = lamp4;
                break;
        }

        StartCoroutine(ITurnLampOffAfterTime(lamp, float.Parse(values[1])));
    }

    private IEnumerator ITurnLampOffAfterTime(ToggleInteractable lamp, float time) {
        yield return new WaitForSeconds(time);

        lamp.Toggle();
        lamp.SetCanBeInteractedWith(true);
    }


}
