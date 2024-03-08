using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationExtensions : MonoBehaviour
{
    private new Animation animation;

    private void Awake() {
        animation = GetComponent<Animation>();
    }

    public void Play(string anim) {
        animation.Play(anim);
    }
}
