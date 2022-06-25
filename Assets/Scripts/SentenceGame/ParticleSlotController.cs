using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSlotController : MonoBehaviour
{
    public List<string> correctParticles = new List<string>();
    public void setParticle(string x)
    {
        correctParticles.Clear();
        correctParticles.Add(x);
    }

    public void setParticle(List<string> xl)
    {
        correctParticles.Clear();
        correctParticles = xl;
    }

    public bool checkSlotChild()
    {
        if (GetComponentInChildren<ParticleCardController>())
        {
            ParticleCardController particleCard = GetComponentInChildren<ParticleCardController>();
            foreach (string i in correctParticles)
            {
                if (particleCard.particle == i)
                    return true;
            }
            
        }
        return false;
    }
}
