using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AndroidController : MonoBehaviourPunCallbacks
{
    public Rigidbody theRB;
    public Rigidbody carRB;

    public float maxSpeed;

    public float forwardAccel = 8f, reverseAccel = 4f;
    private float speedInput, speed;

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

    bool accelerating, reversing, turningRight, turningLeft;

    public TrailRenderer[] trails;

    public AudioSource engineSound;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        return;

        theRB.transform.parent = null;
        carRB.transform.parent = null;

        dragOnGround = theRB.drag;

    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        return;

        //drift
        /*if (Input.GetAxisRaw("Jump") > 0)
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
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }
        }*/

        if (speedInput > 0)
        {
            speed = speedInput * forwardAccel * 1000f;
        }
        else if (speedInput < 0)
        {
            speed = speedInput * reverseAccel * 1000f;
        }

        //turning the wheels


        if (engineSound != null)
        {
            engineSound.pitch = 1f + ((theRB.velocity.magnitude / maxSpeed) * 1.5f);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        return;

        grounded = false;

        RaycastHit hit;
        Vector3 normalTarget = Vector3.zero;


        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            //when on ground rotate to match the normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 6f * Time.deltaTime);
        }
        print(speedInput);
        if (speedInput != 0) 
        {
            Turn();
            Move();
        }

        transform.position = theRB.position;

        carRB.MoveRotation(transform.rotation);
    }

    private void Move()
    {
        if (grounded)
        {
            theRB.drag = dragOnGround;

            theRB.AddForce(transform.forward * speed);
        }
        else
        {
            theRB.drag = .1f;

            theRB.AddForce(-Vector3.up * gravityMod * 100f);
        }

        if (theRB.velocity.magnitude > maxSpeed)
        {
            theRB.velocity = theRB.velocity.normalized * maxSpeed;
        }
        if (grounded && speedInput != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0f));
        }
    }

    private void Turn()
    {
        
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn), rightFrontWheel.localRotation.eulerAngles.z);
    }

    public void TurnLeft()
    {
        turningLeft = true;
        if (turnInput > -1) { turnInput -= Time.deltaTime; }
    }

    public void NotTurningLeft()
    {
        turningLeft = false;
        TurnNeutral();
    }

    public void TurnRight()
    {
        turningRight = true;
        if (turnInput < 1) { turnInput += Time.deltaTime; }
    }

    public void NotTurningRight()
    {
        turningRight = false;
        TurnNeutral();
    }

    public void TurnNeutral()
    {
        if (!turningLeft && !turningRight)
        {
            if (turnInput > 0f) { turnInput -= Time.deltaTime; }
            else if (speedInput < 0f) { turnInput += Time.deltaTime; }
        }
    }

    public void Accelerate()
    {
        if (speedInput <= 1)
        {
            accelerating = true;
            speedInput += Time.deltaTime;
        }
    }

    public void NotAccelerating()
    {
        accelerating = false;
        Neutral();
    }

    public void Neutral()
    {
        if(!accelerating && !reversing)
        {
            if (speedInput > 0f) { speedInput -= Time.deltaTime; }
            else if (speedInput < 0f) { speedInput += Time.deltaTime; }
        }
    }

    public void Reverse()
    {
        if (speedInput >= -1)
        {
            reversing = true;
            speedInput -= Time.deltaTime;
        }
    }

    public void NotReversing()
    {
        reversing = false;
        Neutral();
    }
}
