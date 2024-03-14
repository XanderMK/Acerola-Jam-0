using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationPart2_5 : MonoBehaviour
{
    [SerializeField] private GameObject aberration;
    [SerializeField] private GameObject aberrationMesh;
    [SerializeField] private FollowPlayer aberrationBehavior;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;

    private PlayerMovement playerMovement;
    
    private Rigidbody rb;
    private Animator anim;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Part 3 End")) {
            aberrationMesh.SetActive(false);
            aberrationBehavior.enabled = false;
            playerMovement.enabled = false;
            rb.velocity = Vector3.zero;
            anim.enabled = true;
            anim.Play("Part 3 End");
        }
    }

    public void DisplayText(string textData) {
        string[] parameters = textData.Split(":::");

        HUD.Instance.SetText(parameters[0], float.Parse(parameters[1]), float.Parse(parameters[2]), float.Parse(parameters[3]));
    }

    public void PlayAudioClip(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void PlayMusicClip(AudioClip clip) {
        musicSource.PlayOneShot(clip);
    }

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void TransitionOut(float time) {
        HUD.Instance.TransitionOut(time);
    }

    public void TransitionIn(float time) {
        HUD.Instance.TransitionIn(time);
    }

    public void DisableAbarrition() {
        aberration.SetActive(false);
    }
}
