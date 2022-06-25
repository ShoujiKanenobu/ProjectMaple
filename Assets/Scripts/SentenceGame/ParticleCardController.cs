using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleCardController : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public string particle;
    public void setParticle(string x)
    {
        tmpText.text = x;
        particle = x;
    }
}
