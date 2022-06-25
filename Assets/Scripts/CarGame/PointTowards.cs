using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowards : MonoBehaviour
{
    public float radius;
    public GameObject parent;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        Vector3 point = parent.transform.InverseTransformPoint(target.transform.position);
        point.y = 0.2f;
        transform.position = parent.transform.TransformPoint(point.normalized * radius);
    }
}
