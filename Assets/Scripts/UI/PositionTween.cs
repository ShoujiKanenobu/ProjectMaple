using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTween : MonoBehaviour
{
    public LeanTweenType easeType;
    public GameObject start, end;
    public void ToStartTween()
    {
        LeanTween.move(gameObject, start.transform.position, 0.5f).setEase(easeType);
    }
    public void ToEndTween()
    {
        LeanTween.move(gameObject, end.transform.position, 0.5f).setEase(easeType);
    }

    public void CheatToStart()
    {
        gameObject.transform.position = start.transform.position;
    }
    

}
