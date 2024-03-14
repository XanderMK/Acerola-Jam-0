using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MoveOnToMainMenu : MonoBehaviour
{
    public void OnAny(InputAction.CallbackContext context) {
        if (context.performed) {
            HUD.Instance.TransitionIn(1f);
            StartCoroutine(LoadSceneAfter("Main Menu", 1f));
        }
    }

    private IEnumerator LoadSceneAfter(string scene, float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
