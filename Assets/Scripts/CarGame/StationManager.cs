using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public GameEvent arrivedAtStation;
    public void OnTriggerEnter(Collider other)
    {
        arrivedAtStation.Raise();
    }
}
