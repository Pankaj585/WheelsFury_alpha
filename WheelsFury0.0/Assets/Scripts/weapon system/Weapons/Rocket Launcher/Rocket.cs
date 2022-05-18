using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    const float force = 1000f;
    ParticleSystem blastEffect;
    [SerializeField] WeaponInfo weaponInfo;
    WeaponPool pool;
    SphereCollider sphereCollider;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        blastEffect = GetComponentInChildren<ParticleSystem>();
        pool = FindObjectOfType<WeaponPool>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void Launch(Transform launchTransform)
    {
        transform.position = launchTransform.position;
        transform.forward = launchTransform.forward;
        rb.isKinematic = false;
        sphereCollider.enabled = true;
        rb.AddForce(transform.forward * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        sphereCollider.enabled = false;
        blastEffect.Play();

        if (PhotonNetwork.IsMasterClient)
        {
            other.GetComponent<PlayerReference>()?.playerRoot.GetComponent<Status>().Damage(weaponInfo.damage);
        }
        StopAllCoroutines();
        StartCoroutine(ReturnMissile());
    }

    IEnumerator ReturnMissile()
    {
        while (blastEffect.isPlaying)
            yield return null;

        pool.ReturnMissile(this.gameObject);
    }
}
