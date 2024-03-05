using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProbe : MonoBehaviour, IToggleable
{
    private ReflectionProbe probe;

    private void Awake() {
        probe = GetComponent<ReflectionProbe>();
    }

    public void Toggle(bool dummy) {
        probe.RenderProbe();
    }
}
