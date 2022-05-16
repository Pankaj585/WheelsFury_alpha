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
    [Header("Weapon GFX")]
    [SerializeField] GameObject rocketLauncherGFX;
    [SerializeField] GameObject machineGunGFX;
    [SerializeField] GameObject mineGFX;
    [SerializeField] GameObject shockerGFX;
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
        
        SetWeaponGFX();
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

    void SetWeaponGFX()
    {
        rocketLauncherGFX.SetActive(false);
        machineGunGFX.SetActive(false);
        mineGFX.SetActive(false);
        shockerGFX.SetActive(false);

        switch (weaponInfo.itemIndex)
        {
            case 0: rocketLauncherGFX.SetActive(true);
                break;
            case 1: machineGunGFX.SetActive(true);
                break;
            case 2: mineGFX.SetActive(true);
                break;
            case 3: shockerGFX.SetActive(true);
                break;
            default:
                break;
        }
    }
}
