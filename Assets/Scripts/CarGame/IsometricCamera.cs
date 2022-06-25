using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Vector3 offset;
    public Transform followTarget;

    void FixedUpdate()
    {
        this.gameObject.transform.LookAt(followTarget);
        this.transform.position = followTarget.position + offset;
    }
}
