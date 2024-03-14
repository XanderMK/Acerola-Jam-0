using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float cycleTime;

    private float initialYPos;

    private void Start() {
        initialYPos = transform.position.y;

        transform.DOMoveY(initialYPos + offset, cycleTime/2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
