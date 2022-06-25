using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderWorldUI : MonoBehaviour
{
    public GameObject Canvas;

    void Update()
    {
        Vector3 v = transform.rotation.eulerAngles;
        Canvas.transform.rotation = Quaternion.Euler(90, 0, 180);
    }
}
