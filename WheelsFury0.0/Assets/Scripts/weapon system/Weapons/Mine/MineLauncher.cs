using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MineLauncher : WeaponLauncher
{
    [SerializeField] Transform launchTransform;
    GameHandler gameHandler;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public override void OnFireButtonDown()
    {
        if (!pv.IsMine)
            return;

        int instanceID = poolManager.GetFreeInstanceID(weaponInfo);
        pv.RPC("LaunchMine", RpcTarget.All, instanceID);
    }

    public override void OnFireButtonUp()
    {

    }

    [PunRPC]
    void LaunchMine(int instanceID)
    {
        PoolInstance instance = poolManager.GetInstanceByID(weaponInfo, instanceID);
        instance.instance.SetActive(true);
        Mine mine = instance.instance.GetComponent<Mine>();
        mine.SetMineLauncherReference(this);
        mine.SetPoolInstanceReference(instance);
        mine.Launch(launchTransform);

        itemHandler.currentAmmo--;

        if (pv.IsMine)
        {
            gameHandler.UpdateAmmoUI(itemHandler.currentAmmo);

        }

        if (itemHandler.currentAmmo <= 0)
            itemHandler.UnequipWeapon();
    }

    public void ReturnMine(PoolInstance instance)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        pv.RPC("RPC_ReturnMine", RpcTarget.All, instance.ID);
    }

    [PunRPC]
    void RPC_ReturnMine(int mineID)
    {
        Debug.Log("Returned mine");
        poolManager.ReturnInstanceByID(weaponInfo, mineID);
    }
}
