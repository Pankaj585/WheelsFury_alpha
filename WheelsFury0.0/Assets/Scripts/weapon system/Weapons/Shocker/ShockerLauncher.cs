using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShockerLauncher : WeaponLauncher
{
    [SerializeField] ShockerEffect shockerEffect;
    GameHandler gameHandler;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float shockerDamageRadius = 10f;
    PlayerID playerID;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        playerID = transform.root.GetComponent<PlayerID>();
        
    }

    public override void OnFireButtonDown()
    {
        pv.RPC("UseShocker", RpcTarget.All);
    }

    public override void OnFireButtonUp()
    {

    }

    [PunRPC]
    void UseShocker()
    {
        PoolInstance instance = poolManager.GetInstance(shockerEffect);
        instance.instance.transform.position = transform.position;
        ShockerEffect effect = instance.instance.GetComponent<ShockerEffect>();
        effect.SetPoolInstanceReference(instance);
        effect.gameObject.SetActive(true);
        effect.PlayEffect();

        if (PhotonNetwork.IsMasterClient)
        {
            ShockerDamage();
        }

        itemHandler.currentAmmo--;

        if (pv.IsMine)
        {
            gameHandler.UpdateAmmoUI(itemHandler.currentAmmo);

        }

        if (itemHandler.currentAmmo <= 0)
            itemHandler.UnequipWeapon();
    }

    void ShockerDamage()
    {
        int maxColliders = 4;
        Collider[] colliders = new Collider[maxColliders];
        int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, shockerDamageRadius, colliders, layerMask);
        for(int i = 0; i < numberOfColliders; i++)
        {
            PlayerID player;
            if (!colliders[i].TryGetComponent<PlayerID>(out player))
                player = colliders[i].GetComponent<PlayerReference>()?.playerRoot.GetComponent<PlayerID>();

            if (player == null || player.ID == playerID.ID)
                continue;

            player.GetComponent<Status>()?.Damage(weaponInfo.damage);
        }
    }
}
