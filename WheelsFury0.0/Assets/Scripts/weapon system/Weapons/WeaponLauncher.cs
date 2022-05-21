using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponLauncher : MonoBehaviour
{
    protected InputHandler inputHandler;
    protected ItemHandler itemHandler;
    protected PoolManager poolManager;
    [SerializeField]protected WeaponInfo weaponInfo;
    protected bool isActive;
    [SerializeField] protected GameObject weaponGFX;
    protected PhotonView pv;
    public void Activate()
    {
        if (pv.IsMine)
        {
            inputHandler.FireButtonDownEvent += OnFireButtonDown;
            inputHandler.FireButtonUpEvent += OnFireButtonUp;
        }
        weaponGFX.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive)
            return;

        if (pv.IsMine)
        {
            inputHandler.FireButtonDownEvent -= OnFireButtonDown;
            inputHandler.FireButtonUpEvent -= OnFireButtonUp;
        }
        weaponGFX.SetActive(false);
        isActive = false;
    }
    public virtual void OnFireButtonDown()
    {

    }

    public virtual void OnFireButtonUp()
    {

    }
}
