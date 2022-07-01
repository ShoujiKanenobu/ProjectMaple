using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderWandering : MonoBehaviour
{
    public float wanderRadius;
    public float wanderSpeed;
    public float wanderBuffer;
    public Vector3 startingPoint;
    public Vector3 destination;

    public bool wandering;

    public bool inPlay;

    public Animator anim;

    private void Start()
    {
        startingPoint = this.transform.position;
        inPlay = true;

    }

    void Update()
    {
        if (!inPlay)
        {
            anim.SetFloat("speed", 0);
            return;
        }
            

        if (!wandering)
        {
            if (Random.Range(0f, 1f) < 0.99f)
            {
                wandering = true;
                destination = startingPoint +
                    new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
            }
        }
        else
        {
            anim.SetFloat("speed", 0.2f);
            this.transform.LookAt(destination);
            this.transform.position = Vector3.MoveTowards(transform.position, destination, wanderSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, destination) < wanderBuffer)
                wandering = false;
        }
    }

    public void ChangePlay(bool x)
    {
        inPlay = x;
    }
}
