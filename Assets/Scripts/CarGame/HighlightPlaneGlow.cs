using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPlaneGlow : MonoBehaviour
{
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_Metallic", Mathf.Abs(Mathf.Sin(Time.time % 360)));
    }
}
