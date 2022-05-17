using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLauncher : MonoBehaviour
{
    protected InputHandler inputHandler;
    protected ItemHandler itemHandler;
    protected WeaponPool pool;
    protected bool isActive;
    [SerializeField] protected GameObject weaponGFX;
    
    public void Activate()
    {
        inputHandler.FireButtonDownEvent += OnFireButtonDown;
        inputHandler.FireButtonUpEvent += OnFireButtonUp;
        weaponGFX.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive)
            return;

        inputHandler.FireButtonDownEvent -= OnFireButtonDown;
        inputHandler.FireButtonUpEvent -= OnFireButtonUp;
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
