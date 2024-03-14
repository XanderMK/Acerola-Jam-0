using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathPart3 : MonoBehaviour
{
    private AudioListener listener;
    private PlayerMovement playerMovement;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        listener = Camera.main.GetComponent<AudioListener>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Death Plane")) {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {
        HUD.Instance.TransitionIn(0f);
        listener.enabled = false;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
