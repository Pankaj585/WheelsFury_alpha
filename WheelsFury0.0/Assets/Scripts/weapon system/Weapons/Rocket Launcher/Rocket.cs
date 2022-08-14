using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Rocket : Ammo
{
    Rigidbody rb;
    const float force = 250f, rotationForce = 30f;
    [SerializeField] Effect impactEffect;
    PoolManager poolManager;
    PoolInstance poolInstance;
    Timer timer = new Timer(20);
    Transform launchPosition,target;
    bool launch;
    public Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        launch = false;
        poolManager = FindObjectOfType<PoolManager>();
        
    }

    public void Launch(Transform launchTransform,Transform targetEnemy)
    {
        if (poolInstance == null)
            return;
        launch = true;
        transform.position = launchTransform.position;
        launchPosition = launchTransform;
        target = targetEnemy;

        if (target != null)
        {
            Vector3 direction = target.position - launchPosition.position;
            direction.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = rotationAmount * rotationForce;
            rb.velocity = transform.forward * force;
        }
        else
            rb.AddForce(transform.forward * force);
        print("target null");

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
            poolManager.ReturnInstance(poolInstance);
        }        
    }
    private void FixedUpdate()
    {
        if(launch == false) { return; }

       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        PoolInstance instance = poolManager.GetInstance(impactEffect);
        instance.instance.transform.position = transform.position;
        MissileImpactEffect effect = instance.instance.GetComponent<MissileImpactEffect>();
        effect.SetPoolInstanceReference(instance);
        effect.gameObject.SetActive(true);
        effect.PlayEffect();
        launch = false;

        if (PhotonNetwork.IsMasterClient)
        {
            other.GetComponent<PlayerReference>()?.playerRoot.GetComponent<Status>().Damage(weaponInfo.damage);
        }

        poolManager.ReturnInstance(poolInstance);
    }

}
