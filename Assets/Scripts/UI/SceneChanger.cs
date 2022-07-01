using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextScene;

    public DifficultyHelper d; 

    public void GoNextScene(bool CheckDifficulty)
    {
        if (CheckDifficulty)
        {
            if(d.difficutly != 0)
                SceneManager.LoadScene(nextScene);
        }    
        else
            SceneManager.LoadScene(nextScene);
    }

}
