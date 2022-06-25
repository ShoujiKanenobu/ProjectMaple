using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMask : MonoBehaviour
{
    public GameObject camera;
    public GameObject target;
    public LayerMask mask;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(camera.transform.position, 
            (target.transform.position - camera.transform.position).normalized, 
            out hit, Mathf.Infinity, mask))
        {
            if(hit.collider.gameObject.tag == "spheremask")
            {
                target.transform.localScale = new Vector3(1,1,1);
            }
            else
            {
                target.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
