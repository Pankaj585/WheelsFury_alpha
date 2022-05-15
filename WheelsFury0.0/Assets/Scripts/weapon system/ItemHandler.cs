using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ItemHandler : MonoBehaviour
{    
    OrbSpawner orbSpawner;
    WeaponInfo weaponInfo;
    PlayerID playerID;
    [SerializeField] WeaponInfo[] weaponInfos;
    PhotonView pv;
    private void Awake()
    {
        orbSpawner = FindObjectOfType<OrbSpawner>();
        playerID = transform.root.GetComponent<PlayerID>();
        pv = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryEquipItemFromOrb(int orbIndex)
    {
        Debug.Log("Trying to equip");
       orbSpawner.RequestWeapon(orbIndex, playerID.ID);        
    }

    public void EquipItem(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
        Debug.Log("Equiped item   " + this.weaponInfo.itemName);

        if(pv.IsMine)
            pv.RPC("SyncEquippedWeapon", RpcTarget.Others, weaponInfo.itemIndex);
    }

    [PunRPC]
    void SyncEquippedWeapon(int weaponID)
    {        
        foreach(WeaponInfo info in weaponInfos)
        {
            if(info.itemIndex == weaponID)
            {
                EquipItem(info);
            }
        }
    }
}
