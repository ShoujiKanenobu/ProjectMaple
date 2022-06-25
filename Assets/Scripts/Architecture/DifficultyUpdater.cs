using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyUpdater : MonoBehaviour
{
    public DifficultyHelper dh;

    public void updateDifficulty(int x)
    {
        dh.difficutly = x;
    }
}
