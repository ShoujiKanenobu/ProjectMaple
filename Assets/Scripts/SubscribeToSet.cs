using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeToSet : MonoBehaviour
{
    public GameObjectRTSet set;

    private void OnEnable()
    {
        set.Add(this.gameObject);
    }

    private void OnDisable()
    {
        set.Remove(this.gameObject);
    }
}
