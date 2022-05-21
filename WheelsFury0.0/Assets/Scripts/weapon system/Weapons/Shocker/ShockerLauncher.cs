using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShockerLauncher : WeaponLauncher
{
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    public override void OnFireButtonDown()
    {

    }

    public override void OnFireButtonUp()
    {

    }
}
