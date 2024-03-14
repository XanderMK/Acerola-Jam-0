using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform checkPosition;
    [SerializeField] private float checkRadius;
    [SerializeField] private float checkDistance;
    [SerializeField] private float numOfChecks;
    [SerializeField] private LayerMask checkLayer;

    Rigidbody rb;
    Vector3 lastSafeLocation;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        for (float i = 0; i < 360f; i += 360f / numOfChecks) {
            float angleInRad = i * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angleInRad), 0f, Mathf.Sin(angleInRad));

            dir *= checkRadius;

            if (Physics.Raycast(checkPosition.position + dir, Vector3.down, checkDistance, checkLayer, QueryTriggerInteraction.Ignore)) {
                continue;
            }

            return;
        }

        lastSafeLocation = transform.position;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Death Plane")) {
            StartCoroutine(Respawn(0.75f));
        }
    }

    public IEnumerator Respawn(float transitionTime) {
        HUD.Instance.TransitionIn(transitionTime / 2f);
        yield return new WaitForSeconds(transitionTime / 2f);
        rb.position = lastSafeLocation;
        rb.velocity = Vector3.zero;
        HUD.Instance.TransitionOut(transitionTime / 2f);

    }
}
