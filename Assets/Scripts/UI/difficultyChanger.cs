using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultyChanger : MonoBehaviour
{
    public int difficulty;
    public DifficultyHelper dh;

    public void Change()
    {
        dh.difficutly = difficulty;
    }
}
