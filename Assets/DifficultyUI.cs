using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DifficultyUI : MonoBehaviour
{
    public DifficultyHelper dh;
    public TextMeshProUGUI t;

    public void ChangeText()
    {
        t.text = "�ނ��������F " + dh.difficutly;
    }
}
