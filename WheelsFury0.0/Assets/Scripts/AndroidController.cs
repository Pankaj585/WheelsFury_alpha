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
    private float speedInput;

    public float turnStrength = 180f;
    public float driftTurnStrength, driftStrength;

    [SerializeField] bool grounded;

    public Transform groundRayPoint;
    public LayerMask whatIsGround;
    public float groundRayLength = .75f;

    private float dragOnGround;
    public float gravityMod = 10f;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    [SerializeField] InputHandler inputHandler;

    public TrailRenderer[] trails;

    public AudioSource engineSound, tireSqueal;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            theRB.transform.gameObject.SetActive(false);
            carRB.transform.gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = true;            
            return;
        }
            
        inputHandler = FindObjectOfType<InputHandler>();
        inputHandler.androidController = this;
    }

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
        if (inputHandler.drift && grounded)
        {
            turnStrength = Mathf.MoveTowards(turnStrength, driftTurnStrength, driftStrength * 100 * Time.deltaTime);

            //if (engineSound.isPlaying) { engineSound.Stop(); ; }
            if (!tireSqueal.isPlaying) { tireSqueal.Play(); }

            foreach (var trail in trails)
            {
                trail.emitting = true;
            }
        }
        else
        {
            turnStrength = 400f;
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }

            if (tireSqueal.isPlaying) { tireSqueal.Stop(); }
            //if(!engineSound.isPlaying) { engineSound.Play(); }
        }

        if (engineSound != null) { engineSound.pitch = 1f + ((theRB.velocity.magnitude / maxSpeed) * 2f); }
        if(tireSqueal!= null) { tireSqueal.pitch = 1f + ((theRB.velocity.magnitude / maxSpeed) * 2f); }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

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

        Turn(inputHandler.horizontal);
        Move(inputHandler.vertical);
    }

    private void Move(float input)
    {
        if (input > 0)
        {
            speedInput = input * forwardAccel * 1000f;
        }
        else if (input < 0)
        {
            speedInput = input * reverseAccel * 1000f;
        }

        if (grounded)
        {
            theRB.drag = dragOnGround;

            theRB.AddForce(transform.forward * speedInput);
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

        transform.position = theRB.position;
    }

    private void Turn(float turnInput)
    {
        
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn), rightFrontWheel.localRotation.eulerAngles.z);
        
        if (grounded && speedInput != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0f));
        }

       carRB.MoveRotation(transform.rotation);
    }

    public void Respawn(Vector3 position)
    {
        photonView.RPC("SetPositionRPC", RpcTarget.All, position);
    }

    [PunRPC]
    void SetPositionRPC(Vector3 position)
    {
        if (!photonView.IsMine)
            return;

        theRB.transform.position = position;
        carRB.transform.position = position;
        transform.position = position;
    }
}
