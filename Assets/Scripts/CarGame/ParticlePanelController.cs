using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ParticlePanelController : MonoBehaviour
{
    public StringRTSet particleSet;
    public TextMeshProUGUI uiText;
    public void UpdatePanel()
    {
        uiText.text += particleSet.Items[particleSet.Items.Count - 1] + " ";
    }

    public void ClearPanel()
    {
        uiText.text = "";
    }
}
