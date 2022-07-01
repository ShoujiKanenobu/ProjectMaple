using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{
    public string particle;

    public StringRTSet pickedUpSet;
    public GameEvent RiderPickedUpEvent;
    public SetParticleText text;

    public GameObjectRTSet RTset;

    public void Awake()
    {
        RTset.Add(this.gameObject);   
    }

    public void OnDestroy()
    {
        RTset.Remove(this.gameObject);
    }

    public void SetParticle(string s)
    {
        particle = s;
        text.UpdateParticle(s);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            pickedUpSet.Add(particle);
            RiderPickedUpEvent.Raise();
            AudioManager.instance.Play("RiderPickup");
            Destroy(this.gameObject);
        }
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
