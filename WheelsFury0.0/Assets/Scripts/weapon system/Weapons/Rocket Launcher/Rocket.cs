using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Rocket : Ammo
{
    Rigidbody rb;
    const float force = 500f, rotationForce = 10f;
    [SerializeField] Effect impactEffect;
    PoolManager poolManager;
    PoolInstance poolInstance;
    Timer timer = new Timer(20);
    Transform targetEnemy;
    bool launch;
    PlayerID myID;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        launch = false;
        poolManager = FindObjectOfType<PoolManager>();
        myID = transform.root.GetComponent<PlayerID>();
    }

    public void Launch(Transform launchTransform)
    {
        if (poolInstance == null)
            return;
        launch = true;
        transform.position = launchTransform.position;
        transform.forward = launchTransform.forward;

        

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

        targetEnemy = FindTarget();
        if (targetEnemy != null)
        {
            Vector3 direction = targetEnemy.position - rb.transform.position;
            direction.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = rotationAmount * rotationForce;
            rb.velocity = transform.forward * force;
        }
        else
            rb.AddForce(transform.forward * force);
    }

    private Transform FindTarget()
    {
        Transform target;
        Camera cam = myID.gameObject.GetComponent<PlayerNetworkManager>().cameraObject.GetComponentInChildren<Camera>();
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        List<Transform> enemiesOnScreen = new List<Transform>();
        PlayerID[] ids = FindObjectsOfType<PlayerID>();
        foreach(var plane in planes)
        {
            foreach(var id in ids)
            {
                if (id == myID) { continue; }

                if (plane.GetDistanceToPoint(id.transform.position) < 0) { enemiesOnScreen.Add(id.transform); }
            }
        }
        if (enemiesOnScreen != null)
        {
            target = FindClosestTarget(enemiesOnScreen);
            return target;
        }
        else return null;
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

    private Transform FindClosestTarget(List<Transform> transforms)
    {
        Transform closestTarget = null;
        if (transforms == null)
            return null;

        float closestDistance = Mathf.Infinity;
        foreach (Transform t in transforms)
        {
            float currentDistance = Vector3.Distance(myID.transform.position, t.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestTarget = t.transform;
            }
        }
        return closestTarget;
    }
}
