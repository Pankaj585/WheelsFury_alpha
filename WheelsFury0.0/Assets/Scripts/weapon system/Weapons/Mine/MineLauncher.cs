using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MineLauncher : WeaponLauncher
{
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        pool = FindObjectOfType<WeaponPool>();
    }

    public override void OnFireButtonDown()
    {

    }

    public override void OnFireButtonUp()
    {

    }
}
