using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffToggleable : MonoBehaviour, IToggleable
{
    public void Toggle(bool value) {
        gameObject.SetActive(value);
    }
}
