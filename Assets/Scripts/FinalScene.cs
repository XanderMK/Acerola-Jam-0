using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FinalScene : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private IEnumerator Start()
    {
        source.DOFade(1, 4);
        yield return new WaitForSeconds(8f);
        HUD.Instance.SetText("Thank you for playing.", 1, 4, 1);
        yield return new WaitForSeconds(7f);
        HUD.Instance.TransitionIn(4f);
        source.DOFade(0, 4);
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Main Menu");
    }
}
