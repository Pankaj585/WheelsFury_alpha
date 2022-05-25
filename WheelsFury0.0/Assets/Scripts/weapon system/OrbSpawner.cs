using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OrbSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] WeaponOrb[] weaponOrbs;
    [SerializeField] WeaponInfo[] weapons;
    PhotonView PV;
    List<PlayerID> ids = new List<PlayerID>();
    Information syncInfo;
    private void Awake()
    {        
        PV = GetComponent<PhotonView>();
        syncInfo = new Information(weaponOrbs.Length);

        foreach(WeaponOrb orb in weaponOrbs)
        {
            orb.Ready += SetWeaponOrb;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < syncInfo.Length; i++)
            {
                syncInfo.weaponIndices[i] = Random.Range(0, weapons.Length);
                syncInfo.orbsAvailability[i] = true;
            }

            ConfigureOrbs();
            PV.RPC("SyncOrbs", RpcTarget.Others, syncInfo.weaponIndices, syncInfo.orbsAvailability);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    [PunRPC]
    void SyncOrbs(int[] weaponIndices, bool[] orbsAvailability)
    {       

        for(int i = 0; i < syncInfo.Length; i++)
        {
            syncInfo.weaponIndices[i] = weaponIndices[i];
            syncInfo.orbsAvailability[i] = orbsAvailability[i];
        }

        ConfigureOrbs();
    }

    void ConfigureOrbs()
    {
        for(int i = 0; i < syncInfo.Length; i++)
        {
            weaponOrbs[i].SetWeapon(weapons[syncInfo.weaponIndices[i]]);
            if (!syncInfo.orbsAvailability[i]) { weaponOrbs[i].Disable(); }
        }
    }

    public void RequestWeapon(int orbIndex, int playerID)
    {
        Debug.Log("Request received");
        PV.RPC("HandleRequest", RpcTarget.All, orbIndex, playerID);
    }

    [PunRPC]
    void HandleRequest(int orbIndex, int playerID)
    {       

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Request recieved by master");          
            if (!weaponOrbs[orbIndex].IsOrbAvailable)
                return;
            
            ItemHandler itemHandler = null;

           /* if (ids == null || ids.Length != PhotonNetwork.CurrentRoom.PlayerCount)
                ids = FindObjectsOfType<PlayerID>();*/

            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i].ID == playerID)
                {
                    itemHandler = ids[i].GetComponent<ItemHandler>();
                    break;
                }
            } 

            itemHandler.EquipItem(weaponOrbs[orbIndex].weaponInfo);
            syncInfo.orbsAvailability[orbIndex] = false;
            weaponOrbs[orbIndex].Disable();
            PV.RPC("SyncOrbs", RpcTarget.Others, syncInfo.weaponIndices, syncInfo.orbsAvailability);
        }
    }

    void SetWeaponOrb(int orbIndex)
    {
        int weaponIndex = Random.Range(0, weapons.Length);

        syncInfo.weaponIndices[orbIndex] = weaponIndex;
        syncInfo.orbsAvailability[orbIndex] = true;
        weaponOrbs[orbIndex].SetWeapon(weapons[weaponIndex]);

        PV.RPC("SyncOrbs", RpcTarget.Others, syncInfo.weaponIndices, syncInfo.orbsAvailability);
    }

    public void AddMyReference(PlayerID playerID)
    {
        ids.Add(playerID);
    }

    public void RemoveMyReference(int playerID)
    {
        for(int i = 0; i < ids.Count; i++)
        {
            if(ids[i].ID == playerID)
            {
                ids.RemoveAt(i);
                break;
            }
        }
    }
    private class Information
    {
        public int[] weaponIndices;
        public bool[] orbsAvailability;
        public int Length { get; private set; }
        public Information(int arrayLength)
        {
            weaponIndices = new int[arrayLength];
            orbsAvailability = new bool[arrayLength];
            Length = arrayLength;
        }
    }
}
