using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OrbSpawner : MonoBehaviour
{
    [SerializeField] WeaponOrb[] weaponOrbs;
    [SerializeField] WeaponInfo[] weapons;
    [SerializeField] float spawnWaitTime;
    PhotonView PV;
    int[] weaponIndices;
    bool[] orbsAvailability;
    Timer[] timers;
    private void Awake()
    {        
        PV = GetComponent<PhotonView>();
        weaponIndices = new int[weaponOrbs.Length];
        timers = new Timer[weaponOrbs.Length];
        orbsAvailability = new bool[weaponOrbs.Length];
        for(int i = 0; i < timers.Length; i++)
        {
            timers[i] = new Timer(spawnWaitTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < weaponIndices.Length; i++)
            {
                weaponIndices[i] = Random.Range(0, 4);
                orbsAvailability[i] = true;
            }

            PV.RPC("SyncOrbs", RpcTarget.All, (object)weaponIndices, (object)orbsAvailability);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void SyncOrbs(object weaponIndices, object orbsAvailability)
    {
        int[] indices = (int[])weaponIndices;
        bool[] orbsAvailabilityArray = (bool[])orbsAvailability;
        for(int i = 0; i < this.weaponIndices.Length; i++)
        {
            this.weaponIndices[i] = indices[i];
            this.orbsAvailability[i] = orbsAvailabilityArray[i];
        }
        ConfigureOrbs();
    }

    void ConfigureOrbs()
    {
        for(int i = 0; i < weaponIndices.Length; i++)
        {
            weaponOrbs[i].SetWeapon(weapons[i]);
            if (!orbsAvailability[i]) { weaponOrbs[i].Disable(); }
        }
    }

    public void RequestWeapon(int orbIndex, ItemHandler itemHandler)
    {
        Debug.Log("Request received");
        PV.RPC("HandleRequest", RpcTarget.All, orbIndex, itemHandler);
    }

    [PunRPC]
    void HandleRequest(int orbIndex, ItemHandler itemHandler)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Request recieved by master");
            if (!weaponOrbs[orbIndex].isOrbAvailable)
                return;

            itemHandler.EquipItem(weaponOrbs[orbIndex].weaponInfo);
            orbsAvailability[orbIndex] = false;
            weaponOrbs[orbIndex].Disable();
            PV.RPC("SyncOrbs", RpcTarget.All, (object)weaponIndices, (object)orbsAvailability);
        }
    }
}
