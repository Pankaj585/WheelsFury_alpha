using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class Mine : Ammo
{

    Rigidbody rb;
    Timer timer;
    [SerializeField] Effect detonateEffect;
    [SerializeField] int mineDisappearTime;
    PoolManager poolManager;
    MineLauncher mineLauncher;
    PoolInstance poolInstance;
    private void Awake()
    {
        /*rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;*/
        poolManager = FindObjectOfType<PoolManager>();
        timer = new Timer(mineDisappearTime);
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (!timer.isTimerRunning)
            return;

        if (timer.Tick(Time.deltaTime))
        {
            mineLauncher.ReturnMine(poolInstance);
        }
    }

    public void Launch(Transform launchTransform)
    {
        transform.position = launchTransform.position;
        transform.forward = launchTransform.forward;
        if(PhotonNetwork.IsMasterClient)
            timer.StartTimer();
    }

    public void SetMineLauncherReference(MineLauncher launcher)
    {
        mineLauncher = launcher;
    }

    public void SetPoolInstanceReference(PoolInstance instance)
    {
        poolInstance = instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered mine");
        GameObject player = other.GetComponent<PlayerReference>()?.playerRoot;
        if (player == null)
            return;
        Debug.Log("Player found");
        PoolInstance instance = poolManager.GetInstance(detonateEffect);
        instance.instance.transform.position = transform.position;
        MineImpactEffect effect = instance.instance.GetComponent<MineImpactEffect>();
        effect.SetPoolInstanceReference(instance);
        effect.gameObject.SetActive(true);
        effect.PlayEffect();
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("damaging player");
            player.GetComponent<Status>().Damage(weaponInfo.damage);
            mineLauncher.ReturnMine(poolInstance);
        }
    }
}
