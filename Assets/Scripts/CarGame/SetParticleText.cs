using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetParticleText : MonoBehaviour
{
    // Start is called before the first frame update
    public void UpdateParticle(string x)
    {
        this.GetComponent<TextMeshProUGUI>().text = x;
    }

}
