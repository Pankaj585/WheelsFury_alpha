using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    const float force = 100f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Launch(Transform launchTransform)
    {
        transform.position = launchTransform.position;
        transform.forward = launchTransform.forward;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force);
    }
}
