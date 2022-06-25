using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    public LeanTweenType easeType;
    public void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnOpen()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f).setEase(easeType);
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEase(easeType).setOnComplete(DisableMe);
    }

    public void DisableMe()
    {
        this.gameObject.SetActive(false);
    }

}
