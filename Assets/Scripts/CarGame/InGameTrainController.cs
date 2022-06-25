using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTrainController : MonoBehaviour
{
    public GameObject TrainStart, TrainEnd;
    public float startMoving;
    public void UpdatePosition(float currentTimer, float totalTimer)
    {
        float percentage = 0;
        if (currentTimer >= startMoving)
            percentage = 1;
        percentage = percentage * (currentTimer - startMoving) * 2/100;
        this.transform.position = Vector3.Lerp(TrainStart.transform.position, TrainEnd.transform.position, percentage);
    }
}
