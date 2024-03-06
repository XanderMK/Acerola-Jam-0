using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformExtensions : MonoBehaviour
{

    public void SetRotationX(float newX) {
        transform.eulerAngles = new Vector3(newX, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void SetRotationY(float newY) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);
    }

    public void SetRotationZ(float newZ) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
    }

    public void SetPositionX(float newX) {
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void SetPositionY(float newY) {
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void SetPositionZ(float newZ) {
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    public void SetLocalRotationX(float localX) {
        transform.localEulerAngles = new Vector3(localX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void SetLocalRotationY(float localY) {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, localY, transform.localEulerAngles.z);
    }

    public void SetLocalRotationZ(float localZ) {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, localZ);
    }

    public void SetLocalPositionX(float localX) {
        transform.localPosition = new Vector3(localX, transform.localPosition.y, transform.localPosition.z);
    }

    public void SetLocalPositionY(float localY) {
        transform.localPosition = new Vector3(transform.localPosition.x, localY, transform.localPosition.z);
    }

    public void SetLocalPositionZ(float localZ) {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, localZ);
    }
}
