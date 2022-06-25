using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DifficultyViewer : MonoBehaviour
{
    public DifficultyHelper dh;
    public TextMeshProUGUI t;
    public void updateText()
    {
        t.text = dh.difficutly.ToString();  
    }

    
}
