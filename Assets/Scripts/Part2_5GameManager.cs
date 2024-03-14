using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Part2_5GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chains;
    [SerializeField] private List<ParticleSystem> chainBreakParticles;
    [SerializeField] private AudioSource chainBreakAudioSource;
    [SerializeField] private AudioClip lockBreak, chainBreak;
    [Space(10f)]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform camera;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private Vector3 chainBreakCamPosition;
    [SerializeField] private Vector3 chainBreakCamRotation;
    [SerializeField] private float timeBeforeBreakChain;
    [SerializeField] private List<TextEvent> chainBreakTextEvent1;
    [SerializeField] private List<TextEvent> chainBreakTextEvent2;
    [SerializeField] private List<TextEvent> lockBreakTextEvents;
    [Space(10f)]
    [SerializeField] private Animation lockAnim;
    [SerializeField] private DoorInteractable door;

    [SerializeField] private int numOfChainsBroken = 0;

    [System.Serializable]
    public struct TextEvent {
        public string line;
        public float waitTime;
        public float fadeInTime;
        public float stayTime;
        public float fadeOutTime;
        public AudioClip voiceLine;
    }

    public void BreakChain() {

        playerRb.velocity = Vector3.zero;
        playerMovement.enabled = false;

        numOfChainsBroken++;

        switch (numOfChainsBroken) {
            case 1:
                StartCoroutine(DisplayTextEvents(chainBreakTextEvent1));
                break;
            case 2:
                StartCoroutine(DisplayTextEvents(chainBreakTextEvent2));
                break;
            default:
                lockAnim.Play();
                chainBreakAudioSource.PlayDelayed(1.5f);
                door.SetLocked(false);
                StartCoroutine(DisplayTextEvents(lockBreakTextEvents));
                break;
        }

        
    }

    private IEnumerator DisplayTextEvents(List<TextEvent> textEvents) {
        camera.position = chainBreakCamPosition;
        camera.eulerAngles = chainBreakCamRotation;

        yield return new WaitForSeconds(timeBeforeBreakChain);

        chainBreakAudioSource.PlayOneShot(chainBreak);

        chains[numOfChainsBroken-1].SetActive(false);
        chainBreakParticles[numOfChainsBroken-1].Play();

        foreach (TextEvent textEvent in textEvents) {
            yield return new WaitForSeconds(textEvent.waitTime);
            HUD.Instance.SetText(textEvent.line, textEvent.fadeInTime, textEvent.stayTime, textEvent.fadeOutTime);
            voiceSource.PlayOneShot(textEvent.voiceLine);
            yield return new WaitForSeconds(textEvent.fadeInTime + textEvent.stayTime + textEvent.fadeOutTime);
        }

        camera.localPosition = Vector3.zero;
        camera.localEulerAngles = Vector3.zero;

        playerMovement.enabled = true;
    }

    public void TransitionHUDAfterTime(string times) {
        string[] values = times.Split(":::");

        StartCoroutine(ITransitionHUDAfterTime(float.Parse(values[0]), float.Parse(values[1])));
    }

    private IEnumerator ITransitionHUDAfterTime(float waitTime, float transitionTime) {
        yield return new WaitForSeconds(waitTime);

        HUD.Instance.TransitionIn(transitionTime);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Part 3");
    }
}
