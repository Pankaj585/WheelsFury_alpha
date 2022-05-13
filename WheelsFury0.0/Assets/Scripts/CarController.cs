﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CarController : MonoBehaviourPunCallbacks
{
    public Rigidbody theRB;

    public float maxSpeed;

    public float forwardAccel = 8f, reverseAccel = 4f;
    private float speedInput;

    public float turnStrength = 180f;
    public float driftTurnStrength = 250f;
    private float turnInput;

    [SerializeField] bool grounded;

    public Transform groundRayPoint;
    public LayerMask whatIsGround;
    public float groundRayLength = .75f;

    private float dragOnGround;
    public float gravityMod = 10f;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    public TrailRenderer[] trails;

    public AudioSource engineSound;

    void Start()
    {
        //if (!photonView.IsMine)
           // return;

        theRB.transform.parent = null;

        dragOnGround = theRB.drag;

    }

    void Update()
    {

       // if (!photonView.IsMine)
           // return;

        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        turnInput = Input.GetAxis("Horizontal");

        if(Input.GetAxisRaw("Jump") > 0)
        {
            turnStrength = driftTurnStrength;
            
            foreach (var trail in trails)
            {
                trail.emitting = true;
                print("drift");
            }
        }
        else
        {
            turnStrength = 360f;
            foreach(var trail in trails)
            {
                trail.emitting = false;
            }
        }

        //turning the wheels
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn), rightFrontWheel.localRotation.eulerAngles.z);

        if(engineSound != null)
        {
            engineSound.pitch = 1f + ((theRB.velocity.magnitude / maxSpeed) * 1.5f);
        }
    }

    private void FixedUpdate()
    {
        //if (!photonView.IsMine)
           // return;

        grounded = false;

        RaycastHit hit;
        Vector3 normalTarget = Vector3.zero;


        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            //when on ground rotate to match the normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 6f * Time.deltaTime);
        }

        //accelerates the car
        if (grounded)
        {
            theRB.drag = dragOnGround;

            theRB.AddForce(transform.forward * speedInput * 1000f);
        } else
        {
            theRB.drag = .1f;

            theRB.AddForce(-Vector3.up * gravityMod * 100f);
        }

        if(theRB.velocity.magnitude > maxSpeed)
        {
            theRB.velocity = theRB.velocity.normalized * maxSpeed;
        }
        if (grounded && speedInput != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0f));
        }
       
        transform.position = theRB.position;

    }
}

