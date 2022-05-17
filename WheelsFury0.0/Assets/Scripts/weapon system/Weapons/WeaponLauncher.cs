using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLauncher 
{
    protected InputHandler inputHandler;
    protected ItemHandler itemHandler;
    protected bool isActive;

    public WeaponLauncher(InputHandler inputHandler, ItemHandler itemHandler)
    {
        this.inputHandler = inputHandler;
        this.itemHandler = itemHandler;
    }

    public void Activate()
    {
        inputHandler.FireButtonDownEvent += OnFireButtonDown;
        inputHandler.FireButtonUpEvent += OnFireButtonUp;
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive)
            return;

        inputHandler.FireButtonDownEvent -= OnFireButtonDown;
        inputHandler.FireButtonUpEvent -= OnFireButtonUp;
        isActive = false;
    }
    public void OnFireButtonDown()
    {

    }

    public void OnFireButtonUp()
    {

    }
}
