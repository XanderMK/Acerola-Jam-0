using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2Animation : MonoBehaviour
{

    private Animation anim;
    private PlayerRespawn respawn;

    private void Start() {
        anim = GetComponentInParent<Animation>();
        respawn = GetComponent<PlayerRespawn>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Part 2 Cutscene Trigger")) {
            respawn.enabled = false;
            anim.Play("Fall");
        }
    }
}
