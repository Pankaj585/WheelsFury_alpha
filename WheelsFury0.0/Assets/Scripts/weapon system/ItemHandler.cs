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
    GameHandler gameHandler;
    [SerializeField] WeaponLauncher[] weaponLaunchers;

    [HideInInspector]public int currentAmmo = 0;
    private void Awake()
    {
        orbSpawner = FindObjectOfType<OrbSpawner>();
        playerID = transform.root.GetComponent<PlayerID>();
        pv = GetComponent<PhotonView>();
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public void TryEquipItemFromOrb(int orbIndex)
    {
        Debug.Log("Trying to equip");
       orbSpawner.RequestWeapon(orbIndex, playerID.ID);        
    }

    public void EquipItem(WeaponInfo weaponInfo)
    {        
        pv.RPC("SyncEquippedWeapon", RpcTarget.All, weaponInfo.itemIndex);                 
    }

    void HandleWeaponEquip(WeaponInfo info)
    {
        this.weaponInfo = info;
        currentAmmo = weaponInfo.maxAmmo;
        SetWeaponLauncher();
        if (pv.IsMine)
        {
            gameHandler.SetWeapon(this.weaponInfo);
        }
    }

    [PunRPC]
    void SyncEquippedWeapon(int weaponID)
    {        
        foreach(WeaponInfo info in weaponInfos)
        {
            if(info.itemIndex == weaponID)
            {
                HandleWeaponEquip(info);
            }
        }
    } 

    void SetWeaponLauncher()
    {
        foreach (WeaponLauncher laucher in weaponLaunchers)
            laucher.Deactivate();

        weaponLaunchers[weaponInfo.itemIndex].Activate();
    }

    private void OnDisable()
    {
        foreach (WeaponLauncher laucher in weaponLaunchers)
            laucher.Deactivate();
    }

    public void UnequipWeapon()
    {
        pv.RPC("RPC_UnequipWeapon", RpcTarget.All);
    }

    [PunRPC]
    void RPC_UnequipWeapon()
    {
        currentAmmo = 0;
        weaponLaunchers[weaponInfo.itemIndex].Deactivate();
        weaponInfo = null;

        if (pv.IsMine)
        {
            gameHandler.SetWeapon(weaponInfo);
        }
    }
}
