using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Rocket : Ammo
{
    Rigidbody rb;
    const float force = 1000f;
    [SerializeField] Effect impactEffect;
    PoolManager poolManager;
    PoolInstance poolInstance;
    Timer timer = new Timer(20);
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        poolManager = FindObjectOfType<PoolManager>();
    }

    public void Launch(Transform launchTransform)
    {
        if (poolInstance == null)
            return;

        transform.position = launchTransform.position;
        transform.forward = launchTransform.forward;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force);
        timer.StartTimer();
    }

    public void SetPoolInstanceReference(PoolInstance instance)
    {
        poolInstance = instance;
    }

    private void Update()
    {
        if (!timer.isTimerRunning)
            return;

        if (timer.Tick(Time.deltaTime))
        {
            rb.isKinematic = true;
            poolManager.ReturnInstance(poolInstance);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        PoolInstance instance = poolManager.GetInstance(impactEffect);
        instance.instance.transform.position = transform.position;
        MissileImpactEffect effect = instance.instance.GetComponent<MissileImpactEffect>();
        effect.SetPoolInstanceReference(instance);
        effect.gameObject.SetActive(true);
        effect.PlayEffect();

        if (PhotonNetwork.IsMasterClient)
        {
            other.GetComponent<PlayerReference>()?.playerRoot.GetComponent<Status>().Damage(weaponInfo.damage);
        }

        poolManager.ReturnInstance(poolInstance);
    }
}
