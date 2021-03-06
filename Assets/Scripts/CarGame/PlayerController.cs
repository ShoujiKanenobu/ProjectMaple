using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public CarGameManager cgm;

    public float speed;
    public float maxSpeed;
    public float rotationSpeed;

    private Rigidbody rb;

    public float smokeSpeed;
    public GameObject smokeVFX;

    private float horizontal, vertical;

    public GameObject returnSpot;

    public AudioSource engineSoundSource;
    public float soundThreshold;

    public AudioSource honkSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cgm.inPlay)
        {
            horizontal = 0;
            vertical = 0;
            smokeVFX.SetActive(false);
            return;
        }
            
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (rb.velocity.magnitude >= smokeSpeed)
            smokeVFX.SetActive(true);
        else
            smokeVFX.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space))
            honkSoundSource.Play();
    }

    public void FixedUpdate()
    {
        ApplyForces(horizontal, vertical);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }


        engineSoundSource.pitch = 1 + ((rb.velocity.magnitude / maxSpeed) * 0.5f);

    }

    private void ApplyForces(float horizontal, float vertical)
    {
        if (vertical != 0)
            rb.AddForce(transform.forward * vertical * speed);
        if (horizontal != 0)
            transform.Rotate(Vector3.up * horizontal * rotationSpeed);
    }

    public void returnToGame()
    {
        this.transform.position = returnSpot.transform.position;
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
