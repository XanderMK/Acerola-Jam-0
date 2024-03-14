using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part4TriggerCallbacks : MonoBehaviour
{
    private Animation anim;

    private void Start() {
        anim = GetComponentInParent<Animation>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Good End")) {
            anim.Play("Part 4");
        }
    }
}
