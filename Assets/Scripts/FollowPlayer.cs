using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private AnimationCurve followSpeed;

    private void Update() {
        transform.position += (player.position + followOffset - transform.position).normalized * followSpeed.Evaluate(Vector3.Distance(player.position + followOffset, transform.position)) * Time.deltaTime;
    }
}
